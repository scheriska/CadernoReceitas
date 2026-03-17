using Microsoft.EntityFrameworkCore;
using SocialCook.Aplication.DTOs.Recipes;
using SocialCook.Aplication.Mappings;
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

        public async Task<RecipeResponse> CreateRecipeAsync(CreateRecipeRequest request)
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

            return RecipeMapper.ToRecipeResponse(recipe);
        }

        public async Task<bool> AddImageAsync(Guid recipeId, IFormFile file)
        {
            var recipe = await _context.Recipes.FindAsync(recipeId);
            
            if (recipe == null || recipe.OwnerId != _currentUserService.UserId)
            {
                return false;
            }

            var filename = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var folderPath = Path.Combine("wwwroot/images");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var filePAth = Path.Combine(folderPath, filename);
            using (var stream = new FileStream(filePAth, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var imageUrl = $"/images/{filename}";
            var image = new RecipeImage(recipeId, imageUrl, 0);
            _context.RecipeImages.Add(image);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<RecipeResponse?> GetRecipeByIdAsync(Guid id)
        {
            var recipe = await _context.Recipes
                .Include(r => r.Ingredients)
                    .ThenInclude(ri => ri.Ingredient)
                .Include(r => r.Images)
                .Include(r => r.Categories)
                    .ThenInclude(rc => rc.Category)
                .Include(r => r.Beverages)
                    .ThenInclude(rb => rb.Beverage)
                .FirstOrDefaultAsync(r => r.Id == id);
                
            if (recipe == null)
                return null;

            return RecipeMapper.ToRecipeResponse(recipe);
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

        public async Task<List<RecipeResponse>> GetFeedAsync(int page, int pageSize)
        {
            var recipes = await _context.Recipes
                .Where(r => r.Visibility == RecipeVisibility.Public && r.Status == RecipeStatus.Published)
                .OrderByDescending(r => r.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(r => r.Ingredients)
                    .ThenInclude(ri => ri.Ingredient)
                .Include(r => r.Images)
                .Include(r => r.Categories)
                    .ThenInclude(rc => rc.Category)
                .Include(r => r.Beverages)
                    .ThenInclude(rb => rb.Beverage)
                .ToListAsync();

            return recipes.Select(RecipeMapper.ToRecipeResponse).ToList();
        }

        public async Task<List<RecipeResponse>> GetUserRecipesAsync(Guid userId)
        {
            var recipes = await _context.Recipes
                .Where(r => r.OwnerId == userId)
                .OrderByDescending(r => r.CreatedAt)
                .Include(r => r.Ingredients)
                    .ThenInclude(ri => ri.Ingredient)
                .Include(r => r.Images)
                .Include(r => r.Categories)
                    .ThenInclude(rc => rc.Category)
                .Include(r => r.Beverages)
                    .ThenInclude(rb => rb.Beverage)
                .ToListAsync();

            return recipes.Select(RecipeMapper.ToRecipeResponse).ToList();
        }
    }
}