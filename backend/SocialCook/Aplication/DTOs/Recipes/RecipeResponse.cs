using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialCook.Aplication.DTOs.Recipes
{
    public class RecipeResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string PreparationMethod { get; set; }

        public List<RecipeIngredientResponse> Ingredients { get; set; }
    }

    public class RecipeIngredientResponse
    {
        public string Name { get; set; }

        public string Quantity { get; set; }

        public string Unit { get; set; }
    }
}