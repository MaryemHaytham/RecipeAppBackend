using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RecipeAppBLL.Services;
using RecipeAppBLL.Services.IService;
using RecipeAppDAL.Entity;
using System;
namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService, IWebHostEnvironment webHostEnvironment)
        {
            _recipeService = recipeService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("GetRecipesByName/{recipeName}")]
        public IActionResult GetRecipesByRecipeName(string recipeName)
        {
            try
            {
                return Ok(_recipeService.SearchByName(recipeName));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("GetRecipeByID/{id}")]
        public IActionResult GetRecipeByID(int id) // Changed action name to match method name
        {
            try
            {
                var recipe = _recipeService.GetByID(id);
                if (recipe == null)
                {
                    return StatusCode(404, "Recipe NOT Found");
                }
                return Ok(recipe);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("GetAllRecipes")]
        public IActionResult GetAllRecipes()
        {
            var recipes = _recipeService.GetAllRecipes(); 
            return Ok(recipes);
        }

        

        [HttpPost("AddRecipe")]
        public IActionResult AddRecipe([FromBody] Recipe recipe)
        {
            if (recipe == null)
            {
                return BadRequest("Recipe is empty.");
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

        [HttpPut("UpdateRecipe/{recipeID}")]
        public IActionResult UpdateRecipe(int recipeID, [FromBody] Recipe updatedRecipe)
        {
            try
            {
                _recipeService.UpdateRecipe(updatedRecipe, recipeID);
                return Ok(updatedRecipe);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpPost("UploadImage/{id}")]
        public IActionResult UploadImage(IFormFile imageFile, int id)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("Recipe or image file is missing or empty.");
            }
            string webRootPath = _webHostEnvironment.WebRootPath;
            _recipeService.UploadImage(imageFile, id, webRootPath);
            return Ok();
        }

        [HttpGet("DeleteRecipe/{id}")]
        public IActionResult DeleteRecipe(int id)
        {
            try
            {
                if (_recipeService.DeleteRecipe(id))
                {
                    return Ok("Successfully deleted");
                }

                return StatusCode(404, $"Recipe with ID {id} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
