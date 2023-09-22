using RecipeAppDAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RecipeAppDAL.Repositories.IRepositories
{
    public interface IRecipeRepository :IGenericRepository<Recipe>
    {
        IEnumerable<Recipe> GetRecipesByName(String recipeName);
        //IEnumerable<Recipe> GetRecipesByIngredients(string ingredients);
        //IEnumerable<Recipe> GetRecipesByDietaryRestrictions(string dietaryRestrictions);
        //object getUniqueIngredients();
        IEnumerable<Recipe> SortByRating();
        void AddIngredients(string ingredients);
        IEnumerable<string> GetAllIngredients();
        IEnumerable<Recipe> GetRecipesByIngredient(IEnumerable<string> ingredients);
    }
}

