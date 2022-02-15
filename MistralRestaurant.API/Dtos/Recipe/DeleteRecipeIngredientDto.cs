using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MistralRestaurant.API.Dtos.Recipe
{
    public class DeleteRecipeIngredientDto
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
    }
}
