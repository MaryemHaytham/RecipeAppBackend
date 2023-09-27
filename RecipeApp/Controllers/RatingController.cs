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

        [HttpPost]
        public async Task<IActionResult> CreateRating(RatingDto ratingDto)
        {
            var rating = new Rating
            {
                Value = ratingDto.Value,
                // Set other properties as needed
            };

            var createdRating = await _ratingService.CreateRatingAsync(rating);
            var createdRatingDto = new RatingDto
            {
                Value = createdRating.Value,
                // Map other properties if needed
            };

            return CreatedAtAction(nameof(Rating), new { id = createdRating.RatingId }, createdRatingDto);
        }

        [HttpGet("{recipeId}")]
        public async Task<IActionResult> GetRatingsForRecipe(int recipeId)
        {
            var ratings = await _ratingService.GetRatingsForRecipeAsync(recipeId);
            var ratingDtos = ratings.Select(r => new RatingDto
            {
                Value = r.Value,
                // Map other properties if needed
            }).ToList();

            return Ok(ratingDtos);
        }


    }
}
