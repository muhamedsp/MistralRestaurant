using MistralRestaurant.API.Dtos.Ingredient;
using MistralRestaurant.API.Dtos.RecipeCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MistralRestaurant.API.Dtos.Recipe
{
    public class GetRecipeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double ManufacturingPrice { get; set; }
        public List<GetIngredientDto> Ingredients { get; set; }
        public GetRecipeCategoryDto RecipeCategory { get; set; }
        public int RecipeCategoryId { get; set; }
    }
}
