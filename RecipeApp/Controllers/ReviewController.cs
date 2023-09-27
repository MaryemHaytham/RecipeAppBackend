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
            var review = new Review
            {
                Text = reviewDto.Text,
                // Set other properties as needed
            };

            var createdReview = await _reviewService.CreateReviewAsync(review);
            var createdReviewDto = new ReviewDto
            {
                Text = createdReview.Text,
                // Map other properties if needed
            };

            return CreatedAtAction(nameof(Review), new { id = createdReview.ReviewId }, createdReviewDto);
        }

        [HttpGet("{recipeId}")]
        public async Task<IActionResult> GetReviewsForRecipe(int recipeId)
        {
            var reviews = await _reviewService.GetReviewsForRecipeAsync(recipeId);
            var reviewDtos = reviews.Select(r => new ReviewDto
            {
                Text = r.Text,
                // Map other properties if needed
            }).ToList();

            return Ok(reviewDtos);
        }

    }


}
