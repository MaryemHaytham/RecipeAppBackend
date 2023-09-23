using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeAppDAL.DataContext;
using RecipeAppDAL.Entity;
using RecipeAppDAL.Migrations;
using RecipeAppDAL.Repositories.IRepositories;

namespace RecipeAppDAL.Repositories
{
    public class RecipeRepository : GenericRepository<Recipe>, IRecipeRepository
    {
        private readonly DataContext.RecipeDbContext _recipeDbContext;
        public RecipeRepository(DataContext.RecipeDbContext recipeDbContext) : base(recipeDbContext)
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

        public void AddIngredients(string ingredients)
        {
            // Split the input string into individual ingredients using commas as a delimiter
            var ingredientNames = ingredients.Split(',').Select(i => i.Trim()).Distinct();

            foreach (var ingredientName in ingredientNames)
            {
                // Check if the ingredient already exists in the database
                var existingIngredient = _recipeDbContext.Ingredients.SingleOrDefault(i => i.Name == ingredientName);

                if (existingIngredient == null)
                {
                    // If it doesn't exist, add it to the database
                    var newIngredient = new Ingredients { Name = ingredientName };
                    _recipeDbContext.Ingredients.Add(newIngredient);
                }
            }
            _recipeDbContext.SaveChanges();
        }

        public IEnumerable<string> GetAllIngredients()
        {
            var ingredientNames = _recipeDbContext.Ingredients.Select(i => i.Name).ToList();
            return ingredientNames;
        }

        public IEnumerable<Recipe> GetRecipesByIngredient(IEnumerable<string> ingredients)
        {
            var matchingRecipes = _recipeDbContext.Recipes.ToList();

            foreach (var ingredient in ingredients)
            {
                matchingRecipes = matchingRecipes
                    .Where(recipe => recipe.Ingredients.Contains(ingredient))
                    .ToList();
            }

            return matchingRecipes;
        }

        public IEnumerable<Recipe> getRecipesByCategoryID(int categoryID)
        {
            var matchingRecipes = _recipeDbContext.Recipes
            .Where(r => r.CategoryId == categoryID)
            .ToList();

            return matchingRecipes;
        }
    }
}
