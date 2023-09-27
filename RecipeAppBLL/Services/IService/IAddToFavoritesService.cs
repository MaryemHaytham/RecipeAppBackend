using RecipeAppDTO.FavoritesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppBLL.Services.IService
{
    public interface IAddToFavoritesService
    {
        Task<bool> AddRecipeToFavoritesAsync(AddToFavoriteDto addToFavoriteDto);
        Task<bool> RemoveRecipeFromFavoritesAsync(RemoveFromFavoriteDto removeFromFavoriteDto);
    }
}
