using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeApp.ViewModels;
using RecipeAppBLL.Services.IService;
using RecipeAppDAL.Entity;
using RecipeAppDTO.RecipeDTO;

namespace RecipeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost("CreateReview")]
        public async Task<IActionResult> CreateReview(ReviewDto reviewDto)
        {
            var createdReview = await _reviewService.CreateReviewAsync(reviewDto);

            return Ok(reviewDto);
        }

        [HttpGet("{reviewId}")]
        public async Task<IActionResult> GetReview(int reviewId)
        {
            var review = await _reviewService.GetReviewByIdAsync(reviewId);
            if (review == null)
            {
                return NotFound();
            }

            return Ok(review);
        }

        [HttpGet("GetReviewsForRecipe/{recipeId}")]
        public async Task<IActionResult> GetReviewsForRecipe(int recipeId)
        {
            var reviews = await _reviewService.GetReviewsForRecipeAsync(recipeId);

            return Ok(reviews);
        }

        [HttpPut("{reviewId}")]
        public async Task<IActionResult> UpdateReview(int reviewId, ReviewDto reviewDto)
        {
            await _reviewService.UpdateReviewAsync(reviewId, reviewDto);

            return NoContent();
        }

        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            await _reviewService.DeleteReviewAsync(reviewId);

            return NoContent();
        }
    }


}
