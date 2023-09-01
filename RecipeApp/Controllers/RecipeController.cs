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
    }
}
