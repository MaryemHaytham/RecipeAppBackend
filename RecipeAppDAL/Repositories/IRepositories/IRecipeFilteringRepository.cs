using RecipeAppDAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RecipeAppDAL.Repositories.IRepositories
{
    public interface IRecipeFilteringRepository
    {
        IEnumerable<Recipe> GetRecipesByRecipeName(string recipeName);
        IEnumerable<Recipe> GetRecipesByIngredients(string ingredients);
        IEnumerable<Recipe> GetRecipesByDietaryRestrictions(string dietaryRestrictions);
    }
}

