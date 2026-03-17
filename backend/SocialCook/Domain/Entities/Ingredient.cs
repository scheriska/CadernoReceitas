using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialCook.Domain.Common;

namespace SocialCook.Domain.Entities
{
    public class Ingredient : BaseEntity
    {
        public string Name { get; private set; }
        public string NormalizeName { get; private set; }
        public DateTime CreatedAt { get; set; }

        public List<RecipeIngredient> RecipeIngredients { get; set; }

        private Ingredient() { }

        public Ingredient(string name)
        {
            Name = name;
            NormalizeName = Normalize(name);
            CreatedAt = DateTime.UtcNow;
        }

        private string Normalize(string name)
        {
            return name.Trim().ToLowerInvariant();
        }
    }
}