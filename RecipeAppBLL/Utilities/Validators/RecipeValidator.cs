using RecipeAppBLL.Utilities.CustomExceptions;
using RecipeAppBLL.Utilities.Validators.IValidators;
using RecipeAppDAL.Entity;
using RecipeAppDAL.Repositories;
using RecipeAppDAL.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppBLL.Utilities.Validators
{
    public class RecipeValidator :IRecipeValidator
    {
        IRecipeRepository _recipeRepository;
        public RecipeValidator(IRecipeRepository recipeRepository) {
            this._recipeRepository = recipeRepository;
        }

        public Recipe validateRecipe(int RecipeId)
        {
            Recipe recipe =_recipeRepository.GetById(RecipeId);
            if (recipe == null)
            {
                throw new CustomException("Recipe Not Found");
            }
            return recipe;
        }
    }
}
