using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MistralRestaurant.API.Dtos.Recipe;
using MistralRestaurant.API.Dtos.RecipeCategory;
using MistralRestaurant.API.Models;
using MistralRestaurant.API.Services.RecipeCategoryService;
using MistralRestaurant.API.Services.RecipeServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MistralRestaurant.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RecipeCategoryController : ControllerBase
    {
        private readonly IRecipeCategoryService _recipeCategoryService;
        private readonly IRecipeService _recipeService;

        public RecipeCategoryController(IRecipeCategoryService recipeCategoryService,
                                        IRecipeService recipeService)
        {
            _recipeCategoryService = recipeCategoryService;
            _recipeService = recipeService;
        }

        [HttpGet("GetRecipeCategories")]
        public async Task<ActionResult<ServiceResponse<List<GetRecipeCategoryDto>>>> GetRecipeCategories(int page, int numberItems)
        {
            var listOfRecipes = await _recipeCategoryService.GetRecipeCategoriesByPageAndNumberServices(page, numberItems);

            if (null != listOfRecipes.Data)
            {
                return Ok(listOfRecipes);
            }

            return NotFound(listOfRecipes);
        }

        [HttpGet("{recipeCategoryId}")]
        public async Task<ActionResult<ServiceResponse<List<GetRecipeForRecipeCategoryDto>>>> GetRecipesByCategory(int recipeCategoryId)
        {
            var recipe = await _recipeCategoryService.GetRecipeByCategoryServices(recipeCategoryId);

            if (null != recipe.Data)
            {
                return Ok(recipe);
            }

            return NotFound(recipe);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetRecipeCategoryDto>>>> AddRecipe(AddRecipeCategoryDto recipeCategory)
        {
            var recipe = await _recipeCategoryService.AddRecipeCategoryServices(recipeCategory);

            if (null != recipe.Data)
            {
                return Ok(recipe);
            }

            return NotFound(recipe);
        }

        [HttpPost("Recipe")]
        public async Task<ActionResult<ServiceResponse<List<GetRecipeDto>>>> AddRecipe(AddRecipeDto newRecipe)
        {
            var recipe = await _recipeService.AddRecipeService(newRecipe);

            if (null != recipe.Data)
            {
                return Ok(recipe);
            }

            return NotFound(recipe);
        }

        [HttpGet("Recipe/{recipeId}")]
        public async Task<ActionResult<ServiceResponse<GetRecipeDto>>> GetRecipeById(int recipeId)
        {
            var listOfRecipes = await _recipeService.GetRecipesByIdService(recipeId);

            if (null != listOfRecipes.Data)
            {
                return Ok(listOfRecipes);
            }

            return NotFound(listOfRecipes);
        }

        [HttpGet("GetRecipes/{keyword}")]
        public async Task<ActionResult<ServiceResponse<GetRecipeDto>>> GetRecipeById(string keyword)
        {
            var listOfRecipes = await _recipeService.SearchRecipeService(keyword);

            if (null != listOfRecipes.Data)
            {
                return Ok(listOfRecipes);
            }

            return NotFound(listOfRecipes);
        }

        [HttpPost("Recipe/AddIngeadient")]
        public async Task<ActionResult<ServiceResponse<List<GetRecipeDto>>>> AddIngreadientInRecipe(AddRecipeIngredientDto newIngreadientInRecipe)
        {
            var recipe = await _recipeService.AddIngredientToRecipeService(newIngreadientInRecipe);

            if (null != recipe.Data)
            {
                return Ok(recipe);
            }

            return NotFound(recipe);
        }

        [HttpDelete("Recipe/DeleteIngeadient")]
        public async Task<ActionResult<ServiceResponse<List<GetRecipeDto>>>> DeleteIngreadientInRecipe(DeleteRecipeIngredientDto deleteIngreadientInRecipe)
        {
            var recipe = await _recipeService.DeleteIngredientToRecipeService(deleteIngreadientInRecipe);

            if (null != recipe.Data)
            {
                return Ok(recipe);
            }

            return NotFound(recipe);
        }
    }
}
