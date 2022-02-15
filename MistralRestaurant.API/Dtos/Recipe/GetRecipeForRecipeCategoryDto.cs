using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MistralRestaurant.API.Dtos.Recipe
{
    public class GetRecipeForRecipeCategoryDto
    {
        public string Name { get; set; }
        public double ManufacturingPrice { get; set; }
    }
}
