using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MistralRestaurant.API.Dtos.Recipe
{
    public class AddRecipeDto
    {
        public string Name { get; set; }
        public int RecipeCategoryId { get; set; }
    }
}
