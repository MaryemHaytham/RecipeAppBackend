using RecipeAppDAL.Entity;
using RecipeAppDTO.RecipeDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppBLL.Services.IService
{
    public interface IRatingService
    {
        Task<Rating> CreateRatingAsync(RatingDto ratingDto);
        Task<Rating> GetRatingByIdAsync(int ratingId);
        Task<List<Rating>> GetRatingsForRecipeAsync(int recipeId);
        Task UpdateRatingAsync(int ratingId, RatingDto ratingDto);
        Task DeleteRatingAsync(int ratingId);
    }
}
