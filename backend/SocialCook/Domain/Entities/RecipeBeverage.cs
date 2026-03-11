using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialCook.Domain.Common;

namespace SocialCook.Domain.Entities
{
    public class RecipeBeverage : BaseEntity
    {
        public Guid RecipeId { get; private set; }
        public Guid BeverageId { get; private set; }
        public string? Notes { get; private set; }

        private RecipeBeverage() { }

        public RecipeBeverage(Guid recipeId, Guid beverageId, string? notes)
        {
            RecipeId = recipeId;
            BeverageId = beverageId;
            Notes = notes;
        }
    }
}