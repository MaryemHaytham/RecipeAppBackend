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
        public void UploadImage(IFormFile imageFile, int id, String webRootPath);
        void DeleteRecipe(int id);

        void UpdateRecipe(Recipe recipe, int recipeID);
        object GetUniqueIngredients();
        Recipe GetByID(int recipeID);
        public IEnumerable<Recipe> GetAllRecipes();
        public void DeleteImage(int id, string webRootPath);
    }
}