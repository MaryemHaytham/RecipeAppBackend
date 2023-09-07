using RecipeAppDAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppDAL.Repositories.IRepositories
{
    public interface IRecipeRepository
    {
        Recipe GetById(int id);
        IEnumerable<Recipe> GetRecipesByName(String recipeName);
        IEnumerable<Recipe> GetAllRecipes();
        void AddRecipe(Recipe recipe);
        void UpdateRecipe(Recipe recipe);
        void Delete(Recipe recipe);
    }
}
