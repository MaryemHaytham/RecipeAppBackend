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

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeFilteringRepository _recipeRepository;

        public RecipeController(IRecipeFilteringRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }
        [HttpGet( Name = "GetRecipe")]
        public IActionResult GetRecipesByRecipeName()
        {
            //var matchingRecipes = _recipeRepository.GetRecipesByRecipeName(recipeName);
            // return Ok(matchingRecipes);
            return Ok("found");
        }
    }
}
