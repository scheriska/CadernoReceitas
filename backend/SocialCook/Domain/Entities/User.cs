using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialCook.Domain.Common;
using SocialCook.Domain.Emun;

namespace SocialCook.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; private set; }
        public string Email { get; private set;}
        public string PasswordHash { get; private set; }
        public ProfileType ProfileType { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsActive { get; private set; }  

        private readonly List<Recipe> _recipes = new();
        public IReadOnlyCollection<Recipe> Recipes => _recipes;

        private User() { }

        public User(string name, string email, string passwordHash, ProfileType profileType)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            ProfileType = profileType;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }  

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}