using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialCook.Domain.Common;

namespace SocialCook.Domain.Entities
{
    public class RecipeImage : BaseEntity
    {
        public Guid RecipeId { get; private set; }
        public string Url { get; private set; }
        public int Order { get; private set; }

        //navegation properties
        public Recipe Recipe { get; set; }

        private RecipeImage() { }

        public RecipeImage(Guid recipeId, string url, int order)
        {
            RecipeId = recipeId;
            Url = url;
            Order = order;
        }
    }
}