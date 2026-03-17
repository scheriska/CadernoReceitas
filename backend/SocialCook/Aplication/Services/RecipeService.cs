using Microsoft.EntityFrameworkCore;
using SocialCook.Aplication.DTOs.Recipes;
using SocialCook.Domain.Emun;
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

        public async Task<RecipeResponse?> GetRecipeByIdAsync(Guid id)
        {
            var recipe = await _context.Recipes
                .Include(r => r.Ingredients)
                    .ThenInclude(ri => ri.Ingredient)
                .FirstOrDefaultAsync(r => r.Id == id);
                
            if (recipe == null)
                return null;

            return new RecipeResponse
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Description = recipe.Description,
                PreparationMethod = recipe.PreparationMethod,
                Ingredients = recipe.Ingredients.Select(ri => new RecipeIngredientResponse
                {
                    Name = ri.Ingredient.Name,
                    Quantity = ri.Quantity,
                    Unit = ri.Unit
                }).ToList()
            };
        }

        public async Task<bool> PublishAsync(Guid id, Guid userId)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            
            if (recipe == null || recipe.OwnerId != userId)
            {
                return false;
            }

            recipe.Publish();

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Recipe>> GetFeedAsync(int page, int pageSize)
        {
            return await _context.Recipes
                .Where(r => r.Visibility == RecipeVisibility.Public && r.Status == RecipeStatus.Published)
                .OrderByDescending(r => r.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Recipe>> GetUserRecipesAsync(Guid userId)
        {
            return await _context.Recipes
                .Where(r => r.OwnerId == userId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }
    }
}