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
        Task<Review> CreateReviewAsync(Review review);
        Task<Review> GetReviewByIdAsync(int reviewId);
        Task<List<Review>> GetReviewsForRecipeAsync(int recipeId);
        Task UpdateReviewAsync(Review review);
        Task DeleteReviewAsync(int reviewId);
    }
}
