using Microsoft.AspNetCore.Http;
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
        public void uploadImage(IFormFile imageFile, int id);
        Boolean DeleteRecipe(int id);

        Boolean UpdateRecipe(Recipe recipe,int recipeID);
        object GetUniqueIngredients();
        Recipe GetByID(int recipeID);
        public IEnumerable<Recipe> GetAllRecipes();
    }
}









