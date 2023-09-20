using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeAppDAL.DataContext;
using RecipeAppDAL.Entity;
using RecipeAppDAL.Repositories.IRepositories;

namespace RecipeAppDAL.Repositories
{
    public class RecipeRepository : GenericRepository<Recipe>, IRecipeRepository
    {
        private readonly RecipeDbContext _recipeDbContext;
        public RecipeRepository(RecipeDbContext recipeDbContext) : base(recipeDbContext)
        {
            _recipeDbContext = recipeDbContext;
        }
        // Retrieves recipes by their name.
        public IEnumerable<Recipe> GetRecipesByName(string recipeName)
        {
            return _recipeDbContext.Recipes.Where(r => r.RecipeName.Contains(recipeName)).ToList();
        }
        // Retrieves recipes by their dietary restrictions.
        public IEnumerable<Recipe> GetRecipesByDietaryRestrictions(string dietaryRestrictions)
        {
            return _recipeDbContext.Recipes.Where(r => r.DietaryRestrictions.Contains(dietaryRestrictions)).ToList();
        }
        public IEnumerable<Recipe> SortByRating()
        {
            return _recipeDbContext.Recipes.OrderByDescending(recipe => recipe.Rating).ToList();
        }
    }
}
