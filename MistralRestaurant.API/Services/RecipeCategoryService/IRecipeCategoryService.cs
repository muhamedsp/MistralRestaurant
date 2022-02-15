using MistralRestaurant.API.Dtos.Recipe;
using MistralRestaurant.API.Dtos.RecipeCategory;
using MistralRestaurant.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MistralRestaurant.API.Services.RecipeCategoryService
{
    public interface IRecipeCategoryService
    {
        Task<ServiceResponse<List<GetRecipeCategoryDto>>> GetRecipeCategoriesByPageAndNumberServices(int page, int numberItems);
        Task<ServiceResponse<List<GetRecipeForRecipeCategoryDto>>> GetRecipeByCategoryServices(int recipeCategoryId);
        Task<ServiceResponse<List<GetRecipeCategoryDto>>> AddRecipeCategoryServices(AddRecipeCategoryDto recipeCategory);

    }
}
