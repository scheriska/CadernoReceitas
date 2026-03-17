using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialCook.Domain.Common;

namespace SocialCook.Domain.Entities
{
    public class RecipeCategory : BaseEntity
    {
        public Guid RecipeId { get; private set; }
        public Guid CategoryId { get; private set; }
        public DateTime CreatedAt { get; private set; }

        //navegation properties
        public Recipe Recipe { get; set; }
        public Category Category { get; set; }

        private RecipeCategory() { }

        public RecipeCategory(Guid recipeId, Guid categoryId)
        {
            RecipeId = recipeId;
            CategoryId = categoryId;
            CreatedAt = DateTime.UtcNow;
        }
    }
}