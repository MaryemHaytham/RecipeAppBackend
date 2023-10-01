using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeAppBLL.Services.IService;
using RecipeAppDAL.Entity;
using RecipeAppDTO.RecipeDTO;

namespace RecipeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost("CreateRating")]
        public async Task<IActionResult> CreateRating(RatingDto ratingDto)
        {
            var createdRating = await _ratingService.CreateRatingAsync(ratingDto);

            return Ok(ratingDto);
        }

        [HttpGet("{ratingId}")]
        public async Task<IActionResult> GetRating(int ratingId)
        {
            var rating = await _ratingService.GetRatingByIdAsync(ratingId);
            if (rating == null)
            {
                return NotFound();
            }

            return Ok(rating);
        }

        [HttpGet("GetRatingsForRecipe/{recipeId}")]
        public async Task<IActionResult> GetRatingsForRecipe(int recipeId)
        {
            var ratings = await _ratingService.GetRatingsForRecipeAsync(recipeId);

            return Ok(ratings);
        }

        [HttpPut("{ratingId}")]
        public async Task<IActionResult> UpdateRating(int ratingId, RatingDto ratingDto)
        {
            await _ratingService.UpdateRatingAsync(ratingId, ratingDto);

            return NoContent();
        }

        [HttpDelete("{ratingId}")]
        public async Task<IActionResult> DeleteRating(int ratingId)
        {
            await _ratingService.DeleteRatingAsync(ratingId);

            return NoContent();
        }
    }
}
