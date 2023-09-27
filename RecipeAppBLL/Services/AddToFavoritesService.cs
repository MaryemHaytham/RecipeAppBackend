using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeAppBLL.Services.IService;
using RecipeAppBLL.Utilities.CustomExceptions;
using RecipeAppDAL.DataContext;
using RecipeAppDAL.Entity;
using RecipeAppDTO.FavoritesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppBLL.Services
{
    public class AddToFavoritesService : IAddToFavoritesService
    {
        private readonly RecipeDbContext _context;
        private readonly ILogger<AddToFavoritesService> _logger;

        public AddToFavoritesService(RecipeDbContext context, ILogger<AddToFavoritesService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> AddRecipeToFavoritesAsync(AddToFavoriteDto addToFavoriteDto)
        {
            try
            {
                var user = await _context.Users
                    .Include(u => u.FavoriteRecipes)
                    .FirstOrDefaultAsync(u => u.UserId == addToFavoriteDto.UserId);

                if (user == null)
                {
                    return false; // User not found
                }

                var recipe = await _context.Recipes
                    .FirstOrDefaultAsync(r => r.Id == addToFavoriteDto.RecipeId);

                if (recipe == null)
                {
                    return false; // Recipe not found
                }

                // Check if the recipe is already in favorites
                if (user.FavoriteRecipes.Any(rf => rf.RecipeId == addToFavoriteDto.RecipeId))
                {
                    return false; // Recipe is already in favorites
                }

                // Add the recipe to favorites
                var recipeFavorite = new RecipeFavorite
                {
                    UserId = addToFavoriteDto.UserId,
                    RecipeId = addToFavoriteDto.RecipeId
                };

                _context.Favorites.Add(recipeFavorite);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding recipe to favorites.");
                return false;
            }
        }

        public async Task<bool> RemoveRecipeFromFavoritesAsync(RemoveFromFavoriteDto removeFromFavoriteDto)
        {
            try
            {
                var user = await _context.Users
                    .Include(u => u.FavoriteRecipes)
                    .FirstOrDefaultAsync(u => u.UserId == removeFromFavoriteDto.UserId);

                if (user == null)
                {
                    return false; // User not found
                }

                var recipe = await _context.Recipes
                    .FirstOrDefaultAsync(r => r.Id == removeFromFavoriteDto.RecipeId);

                if (recipe == null)
                {
                    return false; // Recipe not found
                }

                // Check if the recipe is in favorites
                var recipeFavorite = user.FavoriteRecipes.FirstOrDefault(rf => rf.RecipeId == removeFromFavoriteDto.RecipeId);
                if (recipeFavorite == null)
                {
                    return false; // Recipe is not in favorites
                }

                // Remove the recipe from favorites
                user.FavoriteRecipes.Remove(recipeFavorite);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing recipe from favorites.");
                return false;
            }

        }
    }
}
