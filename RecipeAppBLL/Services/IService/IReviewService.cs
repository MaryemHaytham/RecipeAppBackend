using RecipeAppDAL.Entity;
using RecipeAppDTO.RecipeDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppBLL.Services.IService
{
    public interface IReviewService
    {
        Task<Review> CreateReviewAsync(ReviewDto reviewDto);
        Task<Review> GetReviewByIdAsync(int reviewId);
        Task<List<Review>> GetReviewsForRecipeAsync(int recipeId);
        Task UpdateReviewAsync(int reviewId, ReviewDto reviewDto);
        Task DeleteReviewAsync(int reviewId);
    }
}
