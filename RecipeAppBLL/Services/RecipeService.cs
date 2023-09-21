using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeAppBLL.CustomExceptions;
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
        private ISearch _searchStrategy;
        public RecipeService(IRecipeRepository recipeRepository, ILogger<RecipeService> logger)
        {
            _recipeRepository = recipeRepository;
            _logger = logger;
        }

        public IEnumerable<Recipe> SearchByName(string recipeName)
        {
            _searchStrategy = new SearchByName();
            return _searchStrategy.Search(recipeName, _recipeRepository);
        }

        public void AddRecipe(Recipe recipe)
        {
            if (recipe == null)
            {
                throw new ArgumentNullException(nameof(recipe), "Recipe is empty.");
            }

            _recipeRepository.Add(recipe);
        }

        public void UpdateRecipe(Recipe recipe, int recipeID) 
        {
            if (recipe == null)
            {
                throw new ArgumentNullException(nameof(recipe), "Recipe is empty.");
            }
            Recipe oldRecipe = GetByID(recipeID);

            oldRecipe.RecipeName = recipe.RecipeName;
            oldRecipe.Ingredients = recipe.Ingredients;
            oldRecipe.Steps = recipe.Steps;
            oldRecipe.DietaryRestrictions = recipe.DietaryRestrictions;
            _recipeRepository.Update(oldRecipe);
           
        }

        public object GetUniqueIngredients()
        {
            throw new NotImplementedException();
        }

        public Recipe GetByID(int recipeID)
        {
            var recipe = _recipeRepository.GetById(recipeID);

            if (recipe == null)
            {
                throw new RecipeNotFoundException();
            }

            return recipe;
        }

        public IEnumerable<Recipe> GetAllRecipes()
        {
            return _recipeRepository.GetAll();
        }

        public void DeleteRecipe(int id) 
        {
            Recipe recipe = GetByID(id);
            _recipeRepository.Delete(recipe);
        }


        public void DeleteImage(int id, string webRootPath)
        {
            Recipe recipe = GetByID(id);
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
        public void UploadImage(IFormFile imageFile, int id, string webRootPath) 
        {
            var contentType = imageFile.ContentType;
            var allowedImageTypes = new[] { "image/jpg","image/jpeg", "image/png", "image/gif" };
            if (!allowedImageTypes.Contains(contentType))
            {
                throw new InvalidImageTypeException();
            }
            Recipe recipe =GetByID(id);
            DeleteImage(id, webRootPath);   // Delete old image
            if (imageFile != null && imageFile.Length > 0)
            {
                string imageFolderPath = Path.Combine(webRootPath, "images"); 

                // Generate a unique filename for the image using a GUID
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;

                string imagePath = Path.Combine(imageFolderPath, uniqueFileName);

                // Save the image file to the specified path
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }

                recipe.Image = "images/" + uniqueFileName; 
                UpdateRecipe(recipe, recipe.Id);
            }
        }

        public IEnumerable<Recipe> SortByRating()
        {
            return _recipeRepository.SortByRating();
        }
    }




}
