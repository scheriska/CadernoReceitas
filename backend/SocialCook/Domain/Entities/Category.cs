using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialCook.Domain.Common;

namespace SocialCook.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; }
        public string NormalizedName { get; private set; }
        public DateTime CreatedAt { get; private set; }

        //navegation inversa
        public List<RecipeCategory> RecipeCategories { get; set; }

        private Category() { }

        public Category(string name)
        {
            Name = name;
            NormalizedName = Normalize(name);
            CreatedAt = DateTime.UtcNow;
        }

        private string Normalize(string value)
            => value.Trim().ToLowerInvariant();
        }
}