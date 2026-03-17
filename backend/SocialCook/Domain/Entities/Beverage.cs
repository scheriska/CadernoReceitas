using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialCook.Domain.Common;
using SocialCook.Domain.Emun;

namespace SocialCook.Domain.Entities
{
    public class Beverage : BaseEntity
    {
        public string Name { get; private set; }
        public BeverageType Type { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }

        //navegation inversa
        public List<RecipeBeverage> RecipeBeverages { get; set; }
        

        private Beverage() { }

        public Beverage(string name, BeverageType type, string description)
        {
            Name = name;
            Type = type;
            Description = description;
            CreatedAt = DateTime.UtcNow;
        }
    }
}