using RecipeAppBLL.Search;
using RecipeAppBLL.Services.IService;
using RecipeAppDAL.Entity;
using RecipeAppDAL.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppBLL.Services
{
   
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeFilteringRepository _recipeRepository;
        ISearch searchStrategy;//strategy pattern
        public RecipeService(IRecipeFilteringRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }
        public IEnumerable<Recipe> SearchByName(string recipeName)
        {
            searchStrategy = new SearchByName();
            return searchStrategy.Search(recipeName, _recipeRepository);
        }
        public void AddRecipe(Recipe recipe)
        {
            if (recipe == null)
            {
                throw new ArgumentNullException(nameof(recipe));
            }

            _recipeRepository.AddRecipe(recipe);
        }

        public void UpdateRecipe(Recipe recipe)
        {
            if (recipe == null)
            {
                throw new ArgumentNullException(nameof(recipe));
            }

            _recipeRepository.UpdateRecipe(recipe);
        }
    }
}
