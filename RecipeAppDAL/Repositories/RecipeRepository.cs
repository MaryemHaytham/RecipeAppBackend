using Microsoft.EntityFrameworkCore;
using RecipeAppDAL.DataContext;
using RecipeAppDAL.Entity;
using RecipeAppDAL.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppDAL.Repositories
{
    public class RecipeRepository : IRecipeRepository, IRecipeFilteringRepository
    {
        private readonly RecipeDbContext _recipeDbContext;

        public RecipeRepository(RecipeDbContext recipeDbContext)
        {
            _recipeDbContext = recipeDbContext;
        }
        

        // Retrieves a recipe by its unique identifier.
        public Recipe GetById(int id)
        {
            return _recipeDbContext.Recipes.Find(id);
        }

        // Retrieves all recipes.
        public IEnumerable<Recipe> GetAll()
        {
            return _recipeDbContext.Recipes.ToList();
        }
        // Adds a new recipe.
        public void Add(Recipe recipe)
        {
           
        }

        // Updates an existing recipe.
        public void Update(Recipe recipe)
        {
           
        }

        // Deletes a recipe by its unique identifier.
        public void Delete(Recipe recipe)
        {  
            _recipeDbContext.Recipes.Remove(recipe);
            _recipeDbContext.SaveChanges();
        }
        // Retrieves recipes by their name.
        public IEnumerable<Recipe> GetRecipesByRecipeName(string recipeName)
        {
            return _recipeDbContext.Recipes.Where(r => r.RecipeName.Contains(recipeName)).ToList();
        }

        // Retrieves recipes by their ingredients.
        public IEnumerable<Recipe> GetRecipesByIngredients(string ingredients)
        {
            return _recipeDbContext.Recipes.Where(r => r.Ingredients.Contains(ingredients)).ToList();
        }

        // Retrieves recipes by their dietary restrictions.
        public IEnumerable<Recipe> GetRecipesByDietaryRestrictions(string dietaryRestrictions)
        {
            return _recipeDbContext.Recipes.Where(r => r.DietaryRestrictions.Contains(dietaryRestrictions)).ToList();
        }
    }
}
