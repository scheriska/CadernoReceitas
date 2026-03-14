using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialCook.Aplication.DTOs.Recipes
{
    public class CreateRecipeIngredientRequest
    {
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string Unit { get; set; }
    }
}