using AutoMapper;
using MistralRestaurant.API.Dtos.Recipe;
using MistralRestaurant.API.Dtos.RecipeCategory;
using MistralRestaurant.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.Api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RecipeCategory, GetRecipeCategoryDto>();
            CreateMap<AddRecipeCategoryDto, RecipeCategory>();
            CreateMap<Recipe, GetRecipeDto>();
            CreateMap<AddRecipeDto, Recipe>();          
            CreateMap<Recipe, GetRecipeForRecipeCategoryDto>();
            CreateMap<Ingredient, GetRecipeIngredientDto>();

        }
    }
}
