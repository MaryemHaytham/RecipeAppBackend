using Microsoft.AspNetCore.Http;
using RecipeAppDAL.Entity;
using RecipeAppDTO.RecipeDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppBLL.Services.IService
{
    public interface IRecipeService
    {
        public IEnumerable<RecipeDTO> SearchByName(string recipeName);
        public RecipeDTO getRecipeToReturnDTO(Recipe recipe);
        public RecipeDTO AddRecipe(AddRecipeDTO recipeDTO);
        public void UploadImage(IFormFile imageFile, int id, String webRootPath);
        void DeleteRecipe(int id);

        void UpdateRecipe(EditRecipeDTO recipe, int recipeID);
        object GetUniqueIngredients();
        public RecipeWithReviewsDTO GetByIdDTO(int recipeID);
        public Recipe GetByID(int recipeID);
        public IEnumerable<RecipeDTO> GetAllRecipes();
        public void DeleteImage(int id, string webRootPath);
        public IEnumerable<RecipeDTO> SortByRating(List<RecipeDTO> recipes);
        IEnumerable<string> GetAllIngredients();
        IEnumerable<RecipeDTO> GetRecipesByIngredient(IEnumerable<string> ingredients);
        public IEnumerable<Categories> AddCategory(string categoryName);
        public IEnumerable<Categories> GetCategories();
        public IEnumerable<RecipeDTO> GetRecipesByCategory(int categoryID);
        IEnumerable<RecipeDTO> GetUserRecipes(int id);
    }
}