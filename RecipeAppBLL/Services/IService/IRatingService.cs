using RecipeAppDAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppBLL.Services.IService
{
    public interface IRatingService
    {
        Task<Rating> CreateRatingAsync(Rating rating);
        Task<Rating> GetRatingByIdAsync(int ratingId);
        Task<List<Rating>> GetRatingsForRecipeAsync(int recipeId);
        Task UpdateRatingAsync(Rating rating);
        Task DeleteRatingAsync(int ratingId);
    }
}
