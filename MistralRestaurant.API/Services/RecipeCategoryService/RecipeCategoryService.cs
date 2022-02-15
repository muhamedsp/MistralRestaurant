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

namespace MistralRestaurant.API.Services.RecipeCategoryService
{
    public class RecipeCategoryService : IRecipeCategoryService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RecipeCategoryService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<GetRecipeCategoryDto>>> GetRecipeCategoriesByPageAndNumberServices(int page, int numberItems)
        {
            var serviceResponse = new ServiceResponse<List<GetRecipeCategoryDto>>();

            try
            {
                int maxId = page * numberItems;
                int maxIdFromDB = _context.RecipeCategories.Max(r => r.Id);

                if ((page > 0 && numberItems > 0) && maxIdFromDB > ((page - 1) * numberItems))
                {
                    if (maxIdFromDB <= maxId)
                    {
                        maxId = maxIdFromDB;
                    }
                }
                else
                {
                    serviceResponse.Success = false;
                    if (page > 0 && numberItems > 0)
                    { 
                        serviceResponse.Message = "List is empty.";
                    } 
                    else
                    {
                        serviceResponse.Message = "Incorrect input parameters.";
                    }

                    return serviceResponse;
                }

                var dbRecipeCategories = await _context.RecipeCategories.Where(r => r.Id > (page - 1) * numberItems && r.Id <= maxId).ToListAsync();

                serviceResponse.Data = dbRecipeCategories.Select(c => _mapper.Map<GetRecipeCategoryDto>(c)).ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetRecipeForRecipeCategoryDto>>> GetRecipeByCategoryServices(int recipeCategoryId)
        {
            var serviceResponse = new ServiceResponse<List<GetRecipeForRecipeCategoryDto>>();

            try
            {
                var recipeCategory = _context.RecipeCategories.Where(r => r.Id == recipeCategoryId);

                if (recipeCategoryId > 0 && null != recipeCategory)
                {
                    var dbRecipes = await _context.Recipes.Where(r => r.RecipeCategoryId == recipeCategoryId).ToListAsync();

                    if (0 != dbRecipes.Count)
                    {
                        serviceResponse.Data = dbRecipes.Select(c => _mapper.Map<GetRecipeForRecipeCategoryDto>(c)).ToList();
                    }
                    else
                    {
                        serviceResponse.Message = "Recipes not found.";
                    }
                }
                else
                {
                    serviceResponse.Success = false;

                    if (recipeCategoryId > 0)
                    {
                        serviceResponse.Message = "Recipe category not found.";
                    }
                    else
                    {
                        serviceResponse.Message = "Incorrect input parameters.";
                    }

                    return serviceResponse;
                }

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetRecipeCategoryDto>>> AddRecipeCategoryServices(AddRecipeCategoryDto recipeCategory)
        {
            var serviceResponse = new ServiceResponse<List<GetRecipeCategoryDto>>();

            RecipeCategory recipe = _mapper.Map<RecipeCategory>(recipeCategory);

            _context.RecipeCategories.Add(recipe);

            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.RecipeCategories
                                    .Select(c => _mapper.Map<GetRecipeCategoryDto>(c)).ToListAsync();

            return serviceResponse;
        }
    }
}
