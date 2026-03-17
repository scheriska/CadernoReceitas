using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialCook.Domain.Common;

namespace SocialCook.Domain.Entities
{
    public class RecipeIngredient : BaseEntity
    {
        public Guid RecipeId { get; private set; }
        public Guid IngredientId { get; private set; }
        public string Quantity { get; private set; }
        public string Unit { get; private set; }
        //navegation properties
        public Ingredient Ingredient { get; set; }

        private RecipeIngredient() { }

        public RecipeIngredient(Guid recipeId, Guid ingredientId, string quantity, string unit)
        {
            RecipeId = recipeId;
            IngredientId = ingredientId;
            Quantity = quantity;
            Unit = unit;
        }
    }
}