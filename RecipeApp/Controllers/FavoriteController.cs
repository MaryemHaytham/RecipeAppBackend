using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeAppBLL.Services;
using RecipeAppBLL.Services.IService;
using RecipeAppDTO.FavoritesDTO;

namespace RecipeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IAddToFavoritesService _favoriteService;

        public FavoriteController(IAddToFavoritesService favoriteService)
        {
            _favoriteService = favoriteService;
        }
        [HttpPost("addtofavorites")]
        public async Task<IActionResult> AddToFavorite(AddToFavoriteDto addToFavoriteDto)
        {
            if (await _favoriteService.AddRecipeToFavoritesAsync(addToFavoriteDto))
            {
                return Ok("Recipe added to favorites successfully.");
            }

            return BadRequest("Failed to add recipe to favorites.");
        }

        // POST: api/user/removefromfavorites
        [HttpPost("removefromfavorites")]
        public async Task<IActionResult> RemoveFromFavorite(RemoveFromFavoriteDto removeFromFavoriteDto)
        {
            if (await _favoriteService.RemoveRecipeFromFavoritesAsync(removeFromFavoriteDto))
            {
                return Ok("Recipe removed from favorites successfully.");
            }

            return BadRequest("Failed to remove recipe from favorites.");
        }
        [HttpGet("favorites/{userId}")]
        public async Task<IActionResult> GetFavoriteRecipes(int userId)
        {
            var favoriteRecipes = await _favoriteService.GetFavoriteRecipesAsync(userId);
            return Ok(favoriteRecipes);
        }
    }
}
