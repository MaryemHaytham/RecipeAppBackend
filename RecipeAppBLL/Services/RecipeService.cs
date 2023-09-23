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
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IMapper _mapper;
        private ISearch _searchStrategy;
        public RecipeService(IRecipeRepository recipeRepository, ILogger<RecipeService> logger, IMapper mapper, IUserRepository userRepository, ICategoriesRepository categoriesRepository)
        {
            _recipeRepository = recipeRepository;
            _logger = logger;
            _mapper = mapper;
            _userRepository = userRepository;
            _categoriesRepository = categoriesRepository;
        }

        public RecipeDTO AddRecipe(AddRecipeDTO recipeDTO)
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
            var category = _categoriesRepository.findByName(recipeDTO.CategoryName);
            if (category == null)
            {
                throw new CustomException("Category can't be null");
            }

            recipe.CategoryId = category.CategoryId;
            recipe.category = category;

            _recipeRepository.Add(recipe);
            _recipeRepository.AddIngredients(recipe.Ingredients);
            return getRecipeToReturnDTO(recipe);
        }
        public void UpdateRecipe(EditRecipeDTO recipeDTO, int recipeID)
        {
            if (recipeDTO == null)
            {
                throw new ArgumentNullException(nameof(recipeDTO), "Recipe is empty.");
            }
            Recipe newRecipe = _mapper.Map<Recipe>(recipeDTO);
            Recipe oldRecipe = GetByID(recipeID);

            var category = _categoriesRepository.GetById(recipeDTO.CategoryId);
            if (category == null)
            {
                throw new CustomException("Category can't be null");
            }

            oldRecipe.CategoryId = category.CategoryId;
            oldRecipe.category = category;

            oldRecipe.RecipeName = newRecipe.RecipeName;
            oldRecipe.Ingredients = newRecipe.Ingredients;
            oldRecipe.Steps = newRecipe.Steps;
            oldRecipe.DietaryRestrictions = newRecipe.DietaryRestrictions;
            _recipeRepository.Update(oldRecipe);
            _recipeRepository.AddIngredients(oldRecipe.Ingredients);

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
        public RecipeDTO getRecipeToReturnDTO(Recipe recipe)
        {
            return _mapper.Map<RecipeDTO>(recipe);
        }

        public RecipeDTO GetByIdDTO(int recipeID)
        {
            Recipe recipe = GetByID(recipeID);
            return _mapper.Map<RecipeDTO>(recipe);
        }


        public IEnumerable<RecipeDTO> SortByRating(List<RecipeDTO> recipes)
        {
            var sortedRecipes = recipes.OrderByDescending(recipe => recipe.Rating).ToList();
            return _mapper.Map<IEnumerable<RecipeDTO>>(sortedRecipes);
        }
        public IEnumerable<RecipeDTO> SearchByName(string recipeName)
        {
            _searchStrategy = new SearchByName();
            var recipes = _searchStrategy.Search(recipeName, _recipeRepository);
            return _mapper.Map<IEnumerable<RecipeDTO>>(recipes);
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

        public IEnumerable<string> GetAllIngredients()
        {
            return _recipeRepository.GetAllIngredients();
        }

        public IEnumerable<RecipeDTO> GetAllRecipes()
        {
            var recipes = _recipeRepository.GetAll();
            return _mapper.Map<IEnumerable<RecipeDTO>>(recipes);
        }

        public IEnumerable<RecipeDTO> GetRecipesByIngredient(IEnumerable<string> ingredients)
        {
            var matchingRecipes = _recipeRepository.GetRecipesByIngredient(ingredients);
            return _mapper.Map<IEnumerable<RecipeDTO>>(matchingRecipes);
        }

        public IEnumerable<Categories> AddCategory(string categoryName)
        {
            if (_categoriesRepository.GetAll().Any(c => c.CategoryName == categoryName))
            {
                return _categoriesRepository.GetAll();
            }
            Categories category = new Categories();
            if (category != null)
            {
                category.CategoryName = categoryName;

            }
            _categoriesRepository.Add(category);
            return _categoriesRepository.GetAll();
        }
        public IEnumerable<Categories> GetCategories()
        {
            return _categoriesRepository.GetAll();
        }

        public IEnumerable<RecipeDTO> GetRecipesByCategory(int categoryID)
        {
            if (_categoriesRepository.GetById(categoryID) == null)
            {
                throw new CustomException("Category Not Found");
            }

            IEnumerable<RecipeDTO> matchingRecipes = new List<RecipeDTO>();
            IEnumerable<RecipeDTO> recipes = _mapper.Map<IEnumerable<Recipe>, IEnumerable<RecipeDTO>>(_recipeRepository.GetAll());

            foreach (var recipe in recipes)
            {
                if (recipe.CategoryId == categoryID)
                {
                    matchingRecipes = matchingRecipes.Append(recipe);
                }
            }

            return matchingRecipes;
        }

    }
}
        
