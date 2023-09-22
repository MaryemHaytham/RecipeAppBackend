using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RecipeAppBLL.Services;
using RecipeAppBLL.Services.IService;
using RecipeAppDAL.Entity;
using System;
using RecipeApp.ViewModels;
using RecipeAppDAL.Migrations;
using RecipeAppDTO.RecipeDTO;

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
            return Ok(_recipeService.GetByIdDTO(id));
        }

        [HttpGet("GetAllRecipes")]
        public IActionResult GetAllRecipes()
        {
            var recipes = _recipeService.GetAllRecipes(); 
            return Ok(recipes);
        }

        [HttpPost("AddRecipe")]
        public IActionResult AddRecipe([FromBody] AddRecipeDTO addRecipe)
        {

            RecipeToReturnDTO newRecipe=_recipeService.AddRecipe(addRecipe);
            return Ok(newRecipe);   
        }

        [HttpPut("UpdateRecipe/{recipeID}")]
        public IActionResult UpdateRecipe(int recipeID, [FromBody] EditRecipeDTO updatedRecipe)
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

        [HttpGet("SortByRating")]
        public IActionResult SortByRating() {
            return Ok(_recipeService.SortByRating());
        }

        [HttpGet("GetIngredients")]
        public IActionResult GetIngredients()
        {
            return Ok(_recipeService.GetAllIngredients());
        }
        [HttpGet("GetRecipesByIngredients")]
        public IActionResult GetRecipesByIngredients([FromQuery] IEnumerable<string> ingredients)
        {
            return Ok(_recipeService.GetRecipesByIngredient(ingredients));
        }

    }
}
