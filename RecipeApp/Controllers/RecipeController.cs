using RecipeAppDAL.Repositories;
using RecipeAppDAL.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using RecipeAppDAL.Entity;
using Microsoft.AspNetCore.Mvc;
using RecipeAppBLL.Services;
using RecipeAppBLL.Services.IService;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;
        public RecipeController(IRecipeService recipeService)
        {
                _recipeService = recipeService;
        }
        [HttpGet("GetRecipesByName/{recipeName}")]
        public IActionResult GetRecipesByRecipeName(String recipeName)
        {
            var matchingRecipes = _recipeService.SearchByName(recipeName);
            return Ok(matchingRecipes);
            
        }
        [HttpPost]
        public IActionResult AddRecipe([FromBody] Recipe recipe)
        {
            if (recipe == null)
            {
                return BadRequest();
            }

            try
            {
                _recipeService.AddRecipe(recipe);
                return CreatedAtAction(nameof(GetRecipesByRecipeName), new { recipeName = recipe.RecipeName }, recipe);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut("UpdateRecipe/{recipeName}")]
        public IActionResult UpdateRecipe(string recipeName, Recipe updatedRecipe)
        {
            if (recipeName != updatedRecipe.RecipeName)
            {
                return BadRequest("RecipeName in the URL does not match RecipeName in the request body.");
            }

            try
            {
                _recipeService.UpdateRecipe(updatedRecipe);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
