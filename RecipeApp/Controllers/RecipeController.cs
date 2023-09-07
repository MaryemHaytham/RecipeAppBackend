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
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;

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
            try
            {
                var matchingRecipes = _recipeService.SearchByName(recipeName);
                return Ok(matchingRecipes);
            }
            catch (Exception ex)
            { 
                return StatusCode(500, "An error occurred while processing your request.");
            }

        }
        [HttpGet("GetRecipeByID/{id}")]
        public IActionResult GetAllRecipes(int id)
        {
            try
            {
                var uniqueIngredients = _recipeService.GetByID(id);
                if (uniqueIngredients == null)
                {
                    return StatusCode(404, "Recipe NOT Found");
                }
                return Ok(uniqueIngredients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }

        }

        [HttpGet("GetAllRecipes")]
        public IActionResult GetAllRecipes()
        {
            var uniqueIngredients = _recipeService.GetAllRecipes();
            return Ok(uniqueIngredients);
        }
        [HttpPost("UploadImage/{id}")]
        public IActionResult UploadImage(IFormFile imageFile,int id)
        {
             if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("Recipe or image file is missing or empty.");
            }
            _recipeService.uploadImage(imageFile, id);
            return Ok();
        }

        [HttpPost("add")]
        public IActionResult AddRecipe([FromBody] Recipe recipe)
        {
            if (recipe== null)
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

        [HttpGet("DeleteRecipe/{id}")]
        public IActionResult DeleteRecipe(int id)
        {
            try
            {
                if (_recipeService.DeleteRecipe(id)) return Ok("Successfully deleted");

                return StatusCode(404, "Recipe with id " + id + " not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }

        }


    }
}
