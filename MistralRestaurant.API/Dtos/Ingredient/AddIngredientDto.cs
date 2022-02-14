using MistralRestaurant.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MistralRestaurant.API.Dtos.Ingredient
{
    public class AddIngredientDto
    {
        public string Name { get; set; }
        public MeasureType MeasureType { get; set; }
        public double PacketQuantity { get; set; }
        public double PacketQuantityPrice { get; set; }
    }
}
