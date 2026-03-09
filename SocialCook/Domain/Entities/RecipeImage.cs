using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialCook.Domain.Entities
{
    public class RecipeImage
    {
        public Guid RecipeId { get; private set; }
        public string Url { get; private set; }
        public int Order { get; private set; }

        private RecipeImage() { }

        public RecipeImage(Guid recipeId, string url, int order)
        {
            RecipeId = recipeId;
            Url = url;
            Order = order;
        }
    }
}