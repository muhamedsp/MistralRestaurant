using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MistralRestaurant.API.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MeasureType MeasureType { get; set; } = MeasureType.unit;
        public double PacketQuantity { get; set; } = 0;
        public double PacketQuantityPrice { get; set; } = 0;
        public double IngredientQuantity { get; set; }
        public MeasureType IngredientMeasureType { get; set; }
        public double IngredientQuantityPrice { get; set; } = 0;
        public List<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}
