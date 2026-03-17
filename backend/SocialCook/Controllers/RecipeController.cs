using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialCook.Aplication.DTOs.Recipes;
using SocialCook.Aplication.Services;

namespace SocialCook.Controllers
{
    [ApiController]
    [Route("api/recipes")]
    public class RecipeController : ControllerBase
    {
        private readonly RecipeService _recipeService;
        private readonly CurrentUserService _currentUserService;

        public RecipeController(RecipeService recipeService, CurrentUserService currentUserService)
        {
            _recipeService = recipeService;
            _currentUserService = currentUserService;
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateRecipe([FromBody] CreateRecipeRequest request)
        {
            var recipe = await _recipeService.CreateRecipeAsync(request);
            return Ok(recipe);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipe([FromRoute] Guid id)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return Ok(recipe);
        }

        [Authorize]
        [HttpPut("{id}/publish")]
        public async Task<IActionResult> PublishRecipe([FromRoute] Guid id)
        {
            var success = await _recipeService.PublishAsync(id, _currentUserService.UserId);

            if (!success)
                return Forbid();

            return Ok();
        }

        [Authorize]
        [HttpGet("feed")]
        public async Task<IActionResult> GetFeed([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var recipes = await _recipeService.GetFeedAsync(page, pageSize);
            return Ok(recipes);
        }
        
        [Authorize]
        [HttpGet("{id}/recipes")]
        public async Task<IActionResult> GetUserRecipes([FromRoute] Guid id)
        {
            var recipes = await _recipeService.GetUserRecipesAsync(id);
            return Ok(recipes);
        }
    }
}