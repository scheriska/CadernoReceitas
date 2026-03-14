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

        public RecipeController(RecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateRecipe([FromBody] CreateRecipeRequest request)
        {
            var recipe = await _recipeService.CreateRecipeAsync(request);
            return Ok(recipe);
        }

    }
}