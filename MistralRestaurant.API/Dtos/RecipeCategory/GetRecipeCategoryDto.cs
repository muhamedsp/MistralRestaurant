using MistralRestaurant.API.Dtos.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MistralRestaurant.API.Dtos.RecipeCategory
{
    public class GetRecipeCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
