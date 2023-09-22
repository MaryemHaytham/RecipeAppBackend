using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeAppBLL.Utilities.CustomExceptions;
using RecipeAppBLL.Search;
using RecipeAppBLL.Services.IService;
using RecipeAppDAL.Entity;
using RecipeAppDAL.Repositories.IRepositories;
using RecipeAppDTO.RecipeDTO;
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
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private ISearch _searchStrategy;
        public RecipeService(IRecipeRepository recipeRepository, ILogger<RecipeService> logger, IMapper mapper, IUserRepository userRepository)
        {
            _recipeRepository = recipeRepository;
            _logger = logger;
            _mapper = mapper;
            _userRepository = userRepository;
        }



        public Recipe AddRecipe(AddRecipeDTO recipeDTO)
        {
            if (recipeDTO == null)
            {
                throw new ArgumentNullException(nameof(recipeDTO), "Recipe is empty.");
            }
            if (_userRepository.GetById(recipeDTO.UserId) == null)
            {
                throw new CustomException("Invalid User");
            }
            Recipe recipe = _mapper.Map<Recipe>(recipeDTO);

            _recipeRepository.Add(recipe);
            _recipeRepository.AddIngredients(recipe.Ingredients);
            return recipe;
        }
        public void UpdateRecipe(EditRecipeDTO recipeDTO, int recipeID)
        {
            if (recipeDTO == null)
            {
                throw new ArgumentNullException(nameof(recipeDTO), "Recipe is empty.");
            }
            Recipe newRecipe = _mapper.Map<Recipe>(recipeDTO);
            Recipe oldRecipe = GetByID(recipeID);
            oldRecipe.RecipeName = newRecipe.RecipeName;
            oldRecipe.Ingredients = newRecipe.Ingredients;
            oldRecipe.Steps = newRecipe.Steps;
            oldRecipe.DietaryRestrictions = newRecipe.DietaryRestrictions;
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
                throw new CustomException("Recipe not found.");
            }

            return recipe;
        }

        public RecipeToReturnDTO GetByIdDTO(int recipeID)
        {
            Recipe recipe = GetByID(recipeID);
            return _mapper.Map<RecipeToReturnDTO>(recipe);
        }

        public IEnumerable<RecipeToReturnDTO> GetAllRecipes()
        {
            var recipes = _recipeRepository.GetAll();
            return _mapper.Map<IEnumerable<RecipeToReturnDTO>>(recipes);
        }
        public IEnumerable<RecipeToReturnDTO> SortByRating()
        {
            var recipes = _recipeRepository.SortByRating();
            return _mapper.Map<IEnumerable<RecipeToReturnDTO>>(recipes);
        }
        public IEnumerable<RecipeToReturnDTO> SearchByName(string recipeName)
        {
            _searchStrategy = new SearchByName();
            var recipes = _searchStrategy.Search(recipeName, _recipeRepository);
            return _mapper.Map<IEnumerable<RecipeToReturnDTO>>(recipes);
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
            _recipeRepository.Update(recipe);
        }
        public void UploadImage(IFormFile imageFile, int id, string webRootPath)
        {
            var contentType = imageFile.ContentType;
            var allowedImageTypes = new[] { "image/jpg", "image/jpeg", "image/png", "image/gif" };
            if (!allowedImageTypes.Contains(contentType))
            {
                throw new CustomException("Invalid image type. Only JPG, JPEG, PNG, and GIF images are allowed.");
            }
            Recipe recipe = GetByID(id);
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
                _recipeRepository.Update(recipe);
            }


        }

        
    }
}
        
