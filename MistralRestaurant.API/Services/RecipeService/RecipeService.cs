using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MistralRestaurant.API.Data;
using MistralRestaurant.API.Dtos.Recipe;
using MistralRestaurant.API.Dtos.RecipeCategory;
using MistralRestaurant.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MistralRestaurant.API.Services.RecipeServices
{
    public class RecipeService : IRecipeService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RecipeService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<GetRecipeDto>> AddIngredientToRecipeService(AddRecipeIngredientDto addIngredientToRecipe)
        {
            var serviceResponse = new ServiceResponse<GetRecipeDto>();

            try
            {
                Recipe dbRecipe = await _context.Recipes
                                              .Include(r => r.RecipeCategory)
                                              .FirstAsync(r => r.Id == addIngredientToRecipe.RecipeId);

                if (null == dbRecipe)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Recipe not found.";
                    return serviceResponse;
                }

                Ingredient dbIngredient = await _context.Ingredients.FirstAsync(i => i.Id == addIngredientToRecipe.IngredientId);

                if (null == dbIngredient)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Ingredient not found.";
                    return serviceResponse;
                }

                dbIngredient.IngredientMeasureType = addIngredientToRecipe.MeasureType;
                dbIngredient.IngredientQuantity = addIngredientToRecipe.IngredientQuantity;

                double ingredientPrice = GetPriceOfIngredient(addIngredientToRecipe.IngredientQuantity,
                                                              addIngredientToRecipe.MeasureType,
                                                              dbIngredient);

                if (ingredientPrice < 0)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Ingredient Measure types are not comparatable.";
                    return serviceResponse;
                }

                IngredientRecipe ingredientRecipe = new IngredientRecipe()
                {
                    Ingredient = dbIngredient,
                    Recipe = dbRecipe,
                    IngredientId = addIngredientToRecipe.IngredientId,
                    RecipeId = addIngredientToRecipe.RecipeId,
                    IngredientQuantity = addIngredientToRecipe.IngredientQuantity,
                    MeasureType = addIngredientToRecipe.MeasureType
                };

                dbRecipe.ManufacturingPrice += ingredientPrice;

                _context.IngredientaAndRecipes.Add(ingredientRecipe);

                await _context.SaveChangesAsync();

                _context.Recipes.Update(dbRecipe);

                dbIngredient.IngredientMeasureType = MeasureType.unit;
                dbIngredient.IngredientQuantity = 0;

                _context.Ingredients.Update(dbIngredient);

                List<Ingredient> Ingredients = new List<Ingredient>();

                var dbRecipeIngredients = await _context.IngredientaAndRecipes
                                              .Where(r => r.RecipeId == addIngredientToRecipe.RecipeId).ToListAsync();

                foreach (IngredientRecipe item in dbRecipeIngredients)
                {
                    Ingredient ingredient_temp = _context.Ingredients.First(i => i.Id == item.IngredientId);

                    Ingredient ingredient = new Ingredient()
                    {
                        Id = item.Id,
                        IngredientMeasureType = item.MeasureType,
                        IngredientQuantity = item.IngredientQuantity,
                        MeasureType = ingredient_temp.MeasureType,
                        Name = ingredient_temp.Name,
                        PacketQuantity = ingredient_temp.PacketQuantity,
                        PacketQuantityPrice = ingredient_temp.PacketQuantityPrice,
                        Recipes = ingredient_temp.Recipes,
                        IngredientQuantityPrice = GetPriceOfIngredient(item.IngredientQuantity,
                                                                       item.MeasureType,
                                                                       ingredient_temp)
                    };

                    Ingredients.Add(ingredient);
                }

                GetRecipeDto getRecipeDto = new GetRecipeDto()
                {
                    Id = dbRecipe.Id,
                    Name = dbRecipe.Name,
                    ManufacturingPrice = dbRecipe.ManufacturingPrice,
                    RecipeCategory = _mapper.Map<GetRecipeCategoryDto>(dbRecipe.RecipeCategory),
                    RecipeCategoryId = dbRecipe.RecipeCategoryId,
                    Ingredients = Ingredients.Select(c => _mapper.Map<GetRecipeIngredientDto>(c)).ToList()
                };

                serviceResponse.Data = getRecipeDto;

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetRecipeDto>>> AddRecipeService(AddRecipeDto newRecipe)
        {
            var serviceResponse = new ServiceResponse<List<GetRecipeDto>>();

            try
            {
                RecipeCategory recipeCategory = await _context.RecipeCategories.FirstAsync(rC => rC.Id == newRecipe.RecipeCategoryId);

                if (recipeCategory == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Recipe categpry not found.";
                    return serviceResponse;
                }

                Recipe recipe = new Recipe
                {
                    Name = newRecipe.Name,
                    ManufacturingPrice = 0,
                    Ingredients = new List<Ingredient>(),
                    RecipeCategory = recipeCategory,
                    RecipeCategoryId = newRecipe.RecipeCategoryId
                };

                _context.Recipes.Add(recipe);

                await _context.SaveChangesAsync();

                _context.RecipeCategories.Update(recipeCategory);

                var dbRecipes = await _context.Recipes.Where(r => r.RecipeCategoryId == newRecipe.RecipeCategoryId).ToListAsync();

                serviceResponse.Data = dbRecipes.Select(c => _mapper.Map<GetRecipeDto>(c)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetRecipeDto>> GetRecipesByIdService(int recipeId)
        {
            var serviceResponse = new ServiceResponse<GetRecipeDto>();

            try
            {
                var dbRecipe = await _context.Recipes
                                              .Include(r => r.RecipeCategory)
                                              .FirstAsync(r => r.Id == recipeId);

                if (null == dbRecipe)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Recipe not found.";
                }
                else
                {
                    List<Ingredient> Ingredients = new List<Ingredient>();

                    var dbRecipeIngredients = await _context.IngredientaAndRecipes
                                                  .Where(r => r.RecipeId == recipeId).ToListAsync();

                    foreach (IngredientRecipe item in dbRecipeIngredients)
                    {
                        Ingredient ingredient_temp = _context.Ingredients.First(i => i.Id == item.IngredientId);

                        Ingredient ingredient = new Ingredient()
                        {
                            Id = item.Id,
                            IngredientMeasureType = item.MeasureType,
                            IngredientQuantity = item.IngredientQuantity,
                            MeasureType = ingredient_temp.MeasureType,
                            Name = ingredient_temp.Name,
                            PacketQuantity = ingredient_temp.PacketQuantity,
                            PacketQuantityPrice = ingredient_temp.PacketQuantityPrice,
                            Recipes = ingredient_temp.Recipes,
                            IngredientQuantityPrice = GetPriceOfIngredient(item.IngredientQuantity,
                                                                           item.MeasureType,
                                                                           ingredient_temp)
                        };

                        Ingredients.Add(ingredient);
                    }


                    GetRecipeDto getRecipeDto = new GetRecipeDto()
                    {
                        Id = dbRecipe.Id,
                        Name = dbRecipe.Name,
                        ManufacturingPrice = dbRecipe.ManufacturingPrice,
                        RecipeCategory = _mapper.Map<GetRecipeCategoryDto>(dbRecipe.RecipeCategory),
                        RecipeCategoryId = dbRecipe.RecipeCategoryId,
                        Ingredients = Ingredients.Select(c => _mapper.Map<GetRecipeIngredientDto>(c)).ToList()
                    };

                    serviceResponse.Data = getRecipeDto;
                }

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        protected double GetPriceOfIngredient(double ingredientQuantity, 
                                              MeasureType ingredientMeasureType, 
                                              Ingredient ingredient)
        {
            double ingredientPrice = -1;

            if (ingredientMeasureType == ingredient.MeasureType)
            {
                ingredientPrice = ingredient.PacketQuantityPrice * (ingredientQuantity / ingredient.PacketQuantity);
            }
            else if (ingredientMeasureType == MeasureType.g && ingredient.MeasureType == MeasureType.Kg)
            {
                ingredientPrice = ingredient.PacketQuantityPrice * (ingredientQuantity / (1000 * ingredient.PacketQuantity));
            }
            else if (ingredientMeasureType == MeasureType.Kg && ingredient.MeasureType == MeasureType.g)
            {
                ingredientPrice = ingredient.PacketQuantityPrice * ((1000 * ingredientQuantity) / (ingredient.PacketQuantity));
            }
            else if (ingredientMeasureType == MeasureType.mL && ingredient.MeasureType == MeasureType.L)
            {
                ingredientPrice = ingredient.PacketQuantityPrice * (ingredientQuantity / (1000 * ingredient.PacketQuantity));
            }
            else if (ingredientMeasureType == MeasureType.L && ingredient.MeasureType == MeasureType.mL)
            {
                ingredientPrice = ingredient.PacketQuantityPrice * ((1000 * ingredientQuantity) / (ingredient.PacketQuantity));
            }

            return ingredientPrice;
        }

        public async Task<ServiceResponse<GetRecipeDto>> DeleteIngredientToRecipeService(DeleteRecipeIngredientDto deleteIngredientToRecipe)
        {
            var serviceResponse = new ServiceResponse<GetRecipeDto>();

            try
            {
                Recipe dbRecipe = await _context.Recipes
                                            .Include(r => r.RecipeCategory)
                                            .FirstAsync(r => r.Id == deleteIngredientToRecipe.RecipeId);

                if (null == dbRecipe)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Recipe not found.";
                    return serviceResponse;
                }

                Ingredient dbIngredient = await _context.Ingredients
                                        .FirstAsync(i => i.Id == deleteIngredientToRecipe.IngredientId);

                if (null == dbIngredient)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Ingredient not found.";
                    return serviceResponse;
                }

                List<Ingredient> Ingredients = new List<Ingredient>();

                var dbRecipeIngredients = await _context.IngredientaAndRecipes
                                              .Where(r => r.RecipeId == deleteIngredientToRecipe.RecipeId).ToListAsync();

                foreach (IngredientRecipe item in dbRecipeIngredients)
                {
                    Ingredient ingredient_temp = _context.Ingredients.First(i => i.Id == item.IngredientId);

                    Ingredient ingredient = new Ingredient()
                    {
                        Id = item.Id,
                        IngredientMeasureType = item.MeasureType,
                        IngredientQuantity = item.IngredientQuantity,
                        MeasureType = ingredient_temp.MeasureType,
                        Name = ingredient_temp.Name,
                        PacketQuantity = ingredient_temp.PacketQuantity,
                        PacketQuantityPrice = ingredient_temp.PacketQuantityPrice,
                        Recipes = ingredient_temp.Recipes,
                        IngredientQuantityPrice = GetPriceOfIngredient(item.IngredientQuantity,
                                                                           item.MeasureType,
                                                                           ingredient_temp)
                    };

                    if (item.IngredientId == deleteIngredientToRecipe.IngredientId 
                        && item.RecipeId == deleteIngredientToRecipe.RecipeId)
                    {
                        double ingredientPrice = GetPriceOfIngredient(item.IngredientQuantity,
                                                                      item.MeasureType,
                                                                      ingredient_temp);

                        if (ingredientPrice < 0)
                        {
                            serviceResponse.Success = false;
                            serviceResponse.Message = "Ingredient Measure types are not comparatable.";
                            return serviceResponse;
                        }

                        dbRecipe.ManufacturingPrice -= ingredientPrice;

                        _context.IngredientaAndRecipes.Remove(item);
                    }
                    else
                    {
                        Ingredients.Add(ingredient);
                    }   
                }

                await _context.SaveChangesAsync();

                _context.Recipes.Update(dbRecipe);

                _context.Ingredients.Update(dbIngredient);

                GetRecipeDto getRecipeDto = new GetRecipeDto()
                {
                    Id = dbRecipe.Id,
                    Name = dbRecipe.Name,
                    ManufacturingPrice = dbRecipe.ManufacturingPrice,
                    RecipeCategory = _mapper.Map<GetRecipeCategoryDto>(dbRecipe.RecipeCategory),
                    RecipeCategoryId = dbRecipe.RecipeCategoryId,
                    Ingredients = Ingredients.Select(c => _mapper.Map<GetRecipeIngredientDto>(c)).ToList()
                };

                serviceResponse.Data = getRecipeDto;

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetRecipeDto>>> SearchRecipeService(string keyword)
        {
            var serviceResponse = new ServiceResponse<List<GetRecipeDto>>();

            try
            {
                if (keyword.Length < 3)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Keyword shoud contain at least 3 characters.";
                    return serviceResponse;
                }

                List<GetRecipeDto> listWithKeyword = new List<GetRecipeDto>();

                var dbRecipes = await _context.Recipes
                                            .Include(r => r.RecipeCategory).ToListAsync();

                var dbIngredients = await _context.Ingredients.ToListAsync();

                var dbRecipeIngredients = await _context.IngredientaAndRecipes.ToListAsync();


                foreach (var item in dbRecipes)
                {
                    if (item.Name.ToLower().Contains(keyword.ToLower()))
                    {
                        if (!IsRecipeExistInList(listWithKeyword, item.Id))
                        {
                            var recipe = dbRecipes.First(r => r.Id == item.Id);

                            recipe.Ingredients = GetIngredientsByRecipeId(recipe.Id);

                            GetRecipeDto recipeDto = new GetRecipeDto()
                            {
                                Id = recipe.Id,
                                Name = recipe.Name,
                                ManufacturingPrice = recipe.ManufacturingPrice,
                                RecipeCategory = _mapper.Map<GetRecipeCategoryDto>(recipe.RecipeCategory),
                                RecipeCategoryId = recipe.RecipeCategoryId,
                                Ingredients = recipe.Ingredients.Select(c => _mapper.Map<GetRecipeIngredientDto>(c)).ToList()
                            };

                            listWithKeyword.Add(recipeDto);

                            listWithKeyword.Add(_mapper.Map<GetRecipeDto>(item));
                        }
                    }
                }

                foreach (var item in dbIngredients)
                {
                    if (item.Name.ToLower().Contains(keyword.ToLower()))
                    {
                        foreach (var itemRI in dbRecipeIngredients)
                        {
                            if (itemRI.IngredientId == item.Id)
                            {
                                if (!IsRecipeExistInList(listWithKeyword, itemRI.RecipeId))
                                {
                                    var recipe = dbRecipes.First(r => r.Id == itemRI.RecipeId);

                                    recipe.Ingredients = GetIngredientsByRecipeId(recipe.Id);

                                    GetRecipeDto recipeDto = new GetRecipeDto()
                                    {
                                        Id = recipe.Id,
                                        Name = recipe.Name,
                                        ManufacturingPrice = recipe.ManufacturingPrice,
                                        RecipeCategory = _mapper.Map<GetRecipeCategoryDto>(recipe.RecipeCategory),
                                        RecipeCategoryId = recipe.RecipeCategoryId,
                                        Ingredients = recipe.Ingredients.Select(c => _mapper.Map<GetRecipeIngredientDto>(c)).ToList()
                                    };

                                    listWithKeyword.Add(recipeDto);
                                }
                            }
                        }
                    }
                }

                if (0 == listWithKeyword.Count)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "List is empty.";
                    return serviceResponse;
                }

                serviceResponse.Data = listWithKeyword;

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        protected List<Ingredient> GetIngredientsByRecipeId(int recipeId)
        {
            List<Ingredient> Ingredients = new List<Ingredient>();

            var dbRecipeIngredients = _context.IngredientaAndRecipes
                                          .Where(r => r.RecipeId == recipeId).ToList();

            foreach (IngredientRecipe item in dbRecipeIngredients)
            {
                Ingredient ingredient_temp = _context.Ingredients.First(i => i.Id == item.IngredientId);

                Ingredient ingredient = new Ingredient()
                {
                    Id = item.Id,
                    IngredientMeasureType = item.MeasureType,
                    IngredientQuantity = item.IngredientQuantity,
                    MeasureType = ingredient_temp.MeasureType,
                    Name = ingredient_temp.Name,
                    PacketQuantity = ingredient_temp.PacketQuantity,
                    PacketQuantityPrice = ingredient_temp.PacketQuantityPrice,
                    Recipes = ingredient_temp.Recipes,
                    IngredientQuantityPrice = GetPriceOfIngredient(item.IngredientQuantity,
                                                                   item.MeasureType,
                                                                   ingredient_temp)
                };

                Ingredients.Add(ingredient);
            }

            return Ingredients;
        }

        protected bool IsRecipeExistInList(List<GetRecipeDto> list, int recipeId)
        {
            bool isExist = false;

            foreach (var item in list)
            {
                if (item.Id == recipeId)
                {
                    isExist = true;
                    break;
                }
            }

            return isExist;
        }
    }
}
