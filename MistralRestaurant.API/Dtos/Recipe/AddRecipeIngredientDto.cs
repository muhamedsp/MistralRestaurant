using MistralRestaurant.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MistralRestaurant.API.Dtos.Recipe
{
    public class AddRecipeIngredientDto
    {
        public int RecipeId { get; set; }
        public double IngredientQuantity { get; set; } = 0;
        public MeasureType MeasureType { get; set; } = MeasureType.unit;
        public int IngredientId { get; set; }
    }
}
