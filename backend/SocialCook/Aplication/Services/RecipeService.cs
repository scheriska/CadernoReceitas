using Microsoft.EntityFrameworkCore;
using SocialCook.Aplication.DTOs.Recipes;
using SocialCook.Domain.Entities;
using SocialCook.Infrastructure.Data;

namespace SocialCook.Aplication.Services
{
    public class RecipeService
    {
        private readonly AppDbContext _context;
        private readonly CurrentUserService _currentUserService;

        public RecipeService(AppDbContext dbContext, CurrentUserService currentUserService)
        {
            _context = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<Recipe> CreateRecipeAsync(CreateRecipeRequest request)
        {
            var recipe = new Recipe(_currentUserService.UserId, request.Title, request.Description, request.PreparationMethod);

            _context.Recipes.Add(recipe);

            foreach (var ingredientRequest in request.Ingredients)
            {
                var ingredient = await _context.Ingredients.FirstOrDefaultAsync(x => x.NormalizeName == ingredientRequest.Name.ToLower());

                if (ingredient == null)
                {
                    ingredient = new Ingredient(ingredientRequest.Name);
                    _context.Ingredients.Add(ingredient);
                    await _context.SaveChangesAsync(); // Salva para obter o ID do ingrediente
                }
                

                var recipeIngredient = new RecipeIngredient(recipe.Id, ingredient.Id, ingredientRequest.Quantity, ingredientRequest.Unit);

                _context.RecipeIngredients.Add(recipeIngredient);
            }

            await _context.SaveChangesAsync();

            return recipe;
        }
    }
}