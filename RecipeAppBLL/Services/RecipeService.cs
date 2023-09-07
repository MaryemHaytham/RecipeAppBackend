using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
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
        private readonly IRecipeRepository _recipeRepository;
        ISearch searchStrategy;//strategy pattern
        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }
        public IEnumerable<Recipe> SearchByName(string recipeName)
        {
            searchStrategy = new SearchByName();
            return searchStrategy.Search(recipeName, _recipeRepository);
        }
        public void uploadImage(IFormFile imageFile,int id)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                string imageFolderPath = "D:\\images";
                // Generate a unique filename for the image, e.g., using a GUID
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;

                // Combine the unique filename with the path to the image folder
                string imagePath = Path.Combine(imageFolderPath, uniqueFileName);

                // Save the image file to the specified path
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }
                Recipe recipe=_recipeRepository.GetById(id);


                // Store the image URL in the Recipe entity
                recipe.Image = imagePath; // Store the file path as the URL
                UpdateRecipe(recipe, recipe.Id);
             
            }
        }

        public void AddRecipe(Recipe recipe)
        {
            if (recipe == null)
            {
                throw new ArgumentNullException(nameof(recipe));
            }

            _recipeRepository.AddRecipe(recipe);
        }

        public Boolean UpdateRecipe(Recipe recipe, int recipeID)
        {
            if (recipe == null)
            {
                throw new ArgumentNullException(nameof(recipe));
            }
            Recipe oldRecipe = _recipeRepository.GetById(recipeID);
               if(oldRecipe == null)
            {
                return false;
            }
            _recipeRepository.UpdateRecipe(recipe);
            return true;
        }

        public object getuniqueingredients()
        {
            //return _reciperepository.getuniqueingredients();
            throw new NotImplementedException();
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

        public Boolean DeleteRecipe(int id)
        {
            Recipe recipe = _recipeRepository.GetById(id);
            if(recipe==null)
            {
                return false;
            }
            _recipeRepository.Delete(recipe);
            return true;
        }

        public IFormFile GetImage(int id)
        {
            Recipe recipe= _recipeRepository.GetById(id);
            if (recipe == null)
            {
                return null;
            }
            String imagePath = recipe.Image;
            if (!File.Exists(imagePath))
            {
                return null;
            }
            byte[] imageBytes = File.ReadAllBytes(imagePath);

            // Create an IFormFile instance
            IFormFile imageFile = new FormFile(new MemoryStream(imageBytes), 0, imageBytes.Length, "image", Path.GetFileName(imagePath));
            return imageFile;


        }
    }
}
