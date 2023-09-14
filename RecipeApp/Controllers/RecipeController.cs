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
            return Ok(_recipeService.SearchByName(recipeName));
        }

        [HttpGet("GetRecipeByID/{id}")]
        public IActionResult GetRecipeByID(int id) 
        {
            return Ok(_recipeService.GetByID(id));
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
            _recipeService.AddRecipe(recipe);
            return CreatedAtAction(nameof(GetRecipesByRecipeName), new { recipeName = recipe.RecipeName }, recipe);   
        }

        [HttpPut("UpdateRecipe/{recipeID}")]
        public IActionResult UpdateRecipe(int recipeID, [FromBody] Recipe updatedRecipe)
        {
            _recipeService.UpdateRecipe(updatedRecipe, recipeID);
            return Ok(updatedRecipe);
        }
        [HttpPut("UploadImage/{id}")]
        public IActionResult UploadImage(IFormFile imageFile, int id)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            _recipeService.UploadImage(imageFile, id, _webHostEnvironment.WebRootPath);
            return Ok();
        }

        [HttpDelete("DeleteRecipe/{id}")]
        public IActionResult DeleteRecipe(int id)
        {
            _recipeService.DeleteRecipe(id);
            return Ok("Successfully deleted");
            
        }
    }
}
