using SocialCook.Aplication.DTOs.Recipes;
using SocialCook.Domain.Entities;

namespace SocialCook.Aplication.Mappings
{
    public class RecipeMapper
    {
        public static RecipeResponse ToRecipeResponse(Recipe recipe)
        {
            return new RecipeResponse
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Description = recipe.Description,
                PreparationMethod = recipe.PreparationMethod,
                Status = recipe.Status.ToString(),
                Visibility = recipe.Visibility.ToString(),
                CreatedAt = recipe.CreatedAt,
                Ingredients = recipe.Ingredients.Select(ri => new RecipeIngredientResponse
                {
                    Name = ri.Ingredient.Name,
                    Quantity = ri.Quantity,
                    Unit = ri.Unit
                }).ToList(),
                Images = recipe.Images.Select(img => new RecipeImageResponse
                {
                    Url = img.Url,
                    Order = img.Order
                }).ToList(),
                Categories = recipe.Categories.Select(rc => new RecipeCategoryResponse
                {
                    Name = rc.Category.Name
                }).ToList(),
                Beverages = recipe.Beverages.Select(rb => new RecipeBeverageResponse
                {
                    Name = rb.Beverage.Name,
                    Note = rb.Notes
                }).ToList()
            };
        }
    }
}