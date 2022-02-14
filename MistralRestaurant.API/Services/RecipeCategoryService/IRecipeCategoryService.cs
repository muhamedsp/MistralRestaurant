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
        Task<ServiceResponse<List<GetRecipeCategoryDto>>> GetAllRecipeCategoriesServices(int page, int numberItems);
        Task<ServiceResponse<GetRecipeCategoryDto>> GetRecipeCategoryServices(int id);

    }
}
