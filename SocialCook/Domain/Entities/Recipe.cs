using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialCook.Domain.Common;
using SocialCook.Domain.Emun;

namespace SocialCook.Domain.Entities
{
    public class Recipe : BaseEntity
    {
        public Guid OwnerId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string PreparationMethod { get; private set; }
        public RecipeVisibility Visibility { get; private set; }
        public RecipeStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private readonly List<RecipeIngredient> _ingredients = new();
        public IReadOnlyCollection<RecipeIngredient> Ingredients => _ingredients;

        private readonly List<RecipeImage> _images = new();
        public IReadOnlyCollection<RecipeImage> Images => _images;

        private readonly List<RecipeCategory> _categories = new();
        public IReadOnlyCollection<RecipeCategory> Categories => _categories;

        private readonly List<RecipeBeverage> _beverages = new();
        public IReadOnlyCollection<RecipeBeverage> Beverages => _beverages;

        private Recipe() { }

        public Recipe(Guid ownerId, string title, string description, string preparationMethod)
        {
            OwnerId = ownerId;
            Title = title;
            Description = description;
            PreparationMethod = preparationMethod;
            Visibility = RecipeVisibility.Private;
            Status = RecipeStatus.Draft;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Publish()
        {
            if (Status != RecipeStatus.Draft)
                throw new InvalidOperationException("Only draft recipes can be published.");

            Status = RecipeStatus.Published;
            Visibility = RecipeVisibility.Public;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Archive()
        {
            Status = RecipeStatus.Archived;
            Visibility = RecipeVisibility.Private;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddIngredient(Ingredient ingredient, string quantity, string unit)
        {
            _ingredients.Add(new RecipeIngredient(this.Id, ingredient.Id, quantity, unit));
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddImage(string url, int order)
        {
            _images.Add(new RecipeImage(this.Id, url, order));
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddCategory(Category category)
        {
            if (_categories.Any(c => c.CategoryId == category.Id))
                return;

            _categories.Add(new RecipeCategory(this.Id, category.Id));
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddBeverage(Beverage beverage, string? notes)
        {
            if (_beverages.Any(b => b.BeverageId == beverage.Id))
                return;

            _beverages.Add(new RecipeBeverage(this.Id, beverage.Id, notes));
            UpdatedAt = DateTime.UtcNow;
        }
        
    }
}