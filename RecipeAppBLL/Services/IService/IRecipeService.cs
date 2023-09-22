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
        public IEnumerable<RecipeToReturnDTO> SearchByName(string recipeName);
        Recipe AddRecipe(AddRecipeDTO recipeDTO);
        public void UploadImage(IFormFile imageFile, int id, String webRootPath);
        void DeleteRecipe(int id);

        void UpdateRecipe(EditRecipeDTO recipe, int recipeID);
        object GetUniqueIngredients();
        public RecipeToReturnDTO GetByIdDTO(int recipeID);
        public Recipe GetByID(int recipeID);
        public IEnumerable<RecipeToReturnDTO> GetAllRecipes();
        public void DeleteImage(int id, string webRootPath);
        public IEnumerable<RecipeToReturnDTO> SortByRating();
    }
}