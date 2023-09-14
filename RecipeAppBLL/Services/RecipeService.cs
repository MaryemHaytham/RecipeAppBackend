using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeAppBLL.Search;
using RecipeAppBLL.Services.IService;
using RecipeAppDAL.Entity;
using RecipeAppDAL.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppBLL.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly ILogger<RecipeService> _logger;
        private readonly IRecipeRepository _recipeRepository;
        private ISearch _searchStrategy; // Use camelCase for private fields
        public RecipeService(IRecipeRepository recipeRepository, ILogger<RecipeService> logger)
        {
            _recipeRepository = recipeRepository;
            _logger = logger;
        }

        public IEnumerable<Recipe> SearchByName(string recipeName)
        {
            _searchStrategy = new SearchByName(); // Use camelCase for local variables
            return _searchStrategy.Search(recipeName, _recipeRepository);
        }

        public void AddRecipe(Recipe recipe)
        {
            if (recipe == null)
            {
                throw new ArgumentNullException(nameof(recipe));
            }

            _recipeRepository.AddRecipe(recipe);
        }

        public bool UpdateRecipe(Recipe recipe, int recipeID) // Use camelCase for parameter names and return types
        {
            if (recipe == null)
            {
                throw new ArgumentNullException(nameof(recipe));
            }
            Recipe oldRecipe = _recipeRepository.GetById(recipeID);
            if (oldRecipe == null)
            {
                return false;
            }
            _recipeRepository.UpdateRecipe(recipe);
            return true;
        }

        public object GetUniqueIngredients()
        {
            throw new NotImplementedException();
        }

        public Recipe GetByID(int recipeID)
        {
            return _recipeRepository.GetById(recipeID);
        }

        public IEnumerable<Recipe> GetAllRecipes()
        {
            return _recipeRepository.GetAllRecipes();
        }

        public bool DeleteRecipe(int id) // Use camelCase for parameter names and return types
        {
            Recipe recipe = _recipeRepository.GetById(id);
            if (recipe == null)
            {
                return false;
            }
            _recipeRepository.Delete(recipe);
            return true;
        }


        public void UploadImage(IFormFile imageFile, int id, string webRootPath) // Use camelCase for parameter names
        {
            DeleteImage(id, webRootPath);

            if (imageFile != null && imageFile.Length > 0)
            {
                string imageFolderPath = Path.Combine(webRootPath, "images"); // Combine with "images" folder

                // Generate a unique filename for the image, e.g., using a GUID
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;

                // Combine the unique filename with the path to the image folder
                string imagePath = Path.Combine(imageFolderPath, uniqueFileName);

                // Save the image file to the specified path
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }

                Recipe recipe = _recipeRepository.GetById(id);

                // Store the relative image path in the Recipe entity
                recipe.Image = imageFolderPath+"/" + uniqueFileName;
                UpdateRecipe(recipe, recipe.Id);
            }
        }
        public void DeleteImage(int id, string webRootPath)
        {
            Recipe recipe = _recipeRepository.GetById(id);
            if (recipe == null)
            {
                return; // Recipe not found; nothing to delete
            }

            string oldImagePath = recipe.Image;
            if (!string.IsNullOrEmpty(oldImagePath))
            {
                //path to the old image
                string fullOldImagePath = Path.Combine(webRootPath, oldImagePath);

                // Check if the old image file exists and delete it
                if (File.Exists(fullOldImagePath))
                {
                    File.Delete(fullOldImagePath);
                }
            }

            // Clear the image path in the Recipe entity
            recipe.Image = null;
            UpdateRecipe(recipe, recipe.Id);
        }
    }
}
