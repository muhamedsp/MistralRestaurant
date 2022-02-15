using MistralRestaurant.API.Dtos.Recipe;
using MistralRestaurant.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MistralRestaurant.API.Services.RecipeServices
{
    public interface IRecipeService
    {
        Task<ServiceResponse<GetRecipeDto>> GetRecipesByIdService(int recipeId);
        Task<ServiceResponse<List<GetRecipeDto>>> AddRecipeService(AddRecipeDto recipe);
        Task<ServiceResponse<GetRecipeDto>> AddIngredientToRecipeService(AddRecipeIngredientDto addIngredientToRecipe);
        Task<ServiceResponse<GetRecipeDto>> DeleteIngredientToRecipeService(DeleteRecipeIngredientDto deleteIngredientToRecipe);
    }
}
