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
            // Returns recipes with matching names
            return Ok(_recipeService.SearchByName(recipeName));
        }

        [HttpGet("GetRecipeByID/{id}")]
        public IActionResult GetRecipeByID(int id)
        {
            // Returns a specific recipe by ID
            return Ok(_recipeService.GetByIdDTO(id));
        }

        [HttpGet("GetAllRecipes")]
        public IActionResult GetAllRecipes()
        {
            // Returns all recipes
            var recipes = _recipeService.GetAllRecipes();
            return Ok(recipes);
        }

        [HttpPost("AddRecipe")]
        public IActionResult AddRecipe([FromBody] AddRecipeDTO addRecipe)
        {
            // Adds a new recipe
            RecipeDTO newRecipe = _recipeService.AddRecipe(addRecipe);
            return Ok(newRecipe);
        }

        [HttpPut("UpdateRecipe/{recipeID}")]
        public IActionResult UpdateRecipe(int recipeID, [FromBody] EditRecipeDTO updatedRecipe)
        {
            // Updates an existing recipe by ID
            _recipeService.UpdateRecipe(updatedRecipe, recipeID);
            return Ok(updatedRecipe);
        }

        [HttpPut("UploadImage/{id}")]
        public IActionResult UploadImage(IFormFile imageFile, int id)
        {
            // Uploads an image for a recipe by ID
            string webRootPath = _webHostEnvironment.WebRootPath;
            _recipeService.UploadImage(imageFile, id, _webHostEnvironment.WebRootPath);
            return Ok();
        }

        [HttpDelete("DeleteRecipe/{id}")]
        public IActionResult DeleteRecipe(int id)
        {
            // Deletes a recipe by ID
            _recipeService.DeleteRecipe(id);
            return Ok(new { message = "Recipe was successfully deleted." });
        }

        [HttpPost("SortByRating")]
        public IActionResult SortByRating([FromBody] List<RecipeDTO> recipes)
        {
            // Sorts recipes by rating and returns the sorted list
            return Ok(_recipeService.SortByRating(recipes));
        }

        [HttpGet("GetIngredients")]
        public IActionResult GetIngredients()
        {
            // Returns a list of all available ingredients
            return Ok(_recipeService.GetAllIngredients());
        }

        [HttpGet("GetRecipesByIngredients")]
        public IActionResult GetRecipesByIngredients([FromQuery] IEnumerable<string> ingredients)
        {
            // Returns recipes that contain specific ingredients
            return Ok(_recipeService.GetRecipesByIngredient(ingredients));
        }

        [HttpPost("AddCategory")]
        public IActionResult AddCategory(string categoryName)
        {
            // Adds a new category
            return Ok(_recipeService.AddCategory(categoryName));
        }

        [HttpGet("GetCategory")]
        public IActionResult GetCategories()
        {
            // Returns a list of all available categories
            return Ok(_recipeService.GetCategories());
        }

        [HttpGet("GetRecipesByCategory/{categoryID}")]
        public IActionResult GetRecipesByCategory(int categoryID)
        {
            // Returns recipes that belong to a specific category
            return Ok(_recipeService.GetRecipesByCategory(categoryID));
        }
        [HttpGet("GetUserRecipes/{userID}")]
        public IActionResult GetUserRecipes(int userID)
        {
            return Ok(_recipeService.GetUserRecipes(userID));
        }
    }
}
