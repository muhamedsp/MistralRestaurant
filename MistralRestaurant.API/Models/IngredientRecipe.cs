using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MistralRestaurant.API.Models
{
    public class IngredientRecipe
    {
        public int Id { get; set; }
        public Recipe Recipe { get; set; }
        public int RecipeId { get; set; }
        public double IngredientQuantity { get; set; }
        public MeasureType MeasureType { get; set; }
        public Ingredient Ingredient { get; set; }
        public int IngredientId { get; set; }
        
    }
}
