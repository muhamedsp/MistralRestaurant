using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MistralRestaurant.API.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double ManufacturingPrice { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public RecipeCategory RecipeCategory { get; set; }
        public int RecipeCategoryId { get; set; }

    }
}
