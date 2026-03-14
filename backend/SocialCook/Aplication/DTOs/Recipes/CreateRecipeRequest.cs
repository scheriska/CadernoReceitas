using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialCook.Aplication.DTOs.Recipes
{
    public class CreateRecipeRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PreparationMethod { get; set; }
        public List<CreateRecipeIngredientRequest> Ingredients { get; set; }
    }
}