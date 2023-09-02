using RecipeAppDAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppBLL.Services.IService
{
    public interface IRecipeService
    {
        public IEnumerable<Recipe> SearchByName(string recipeName);
        void AddRecipe(Recipe recipe);

        void UpdateRecipe(Recipe recipe);
    }
}
