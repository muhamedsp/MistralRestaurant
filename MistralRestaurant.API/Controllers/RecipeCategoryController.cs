using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MistralRestaurant.API.Dtos.RecipeCategory;
using MistralRestaurant.API.Models;
using MistralRestaurant.API.Services.RecipeCategoryService;
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

        public RecipeCategoryController(IRecipeCategoryService recipeCategoryService)
        {
            _recipeCategoryService = recipeCategoryService;
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetRecipeCategoryDto>>>> Get(int page, int numberItems)
        {
            var listOfRecipes = await _recipeCategoryService.GetAllRecipeCategoriesServices(page, numberItems);

            if (null != listOfRecipes.Data)
            {
                return Ok(listOfRecipes);
            }

            return NotFound(listOfRecipes);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetRecipeCategoryDto>>>> GetRecipe(int id)
        {
            var recipe = await _recipeCategoryService.GetRecipeCategoryServices(id);

            if (null != recipe.Data)
            {
                return Ok(recipe);
            }

            return NotFound(recipe);
        }
    }
    
}
