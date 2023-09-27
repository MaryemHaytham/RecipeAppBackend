using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppDTO.FavoritesDTO
{
    public class RemoveFromFavoriteDto
    {
        public int UserId { get; set; }
        public int RecipeId { get; set; }
    }
}
