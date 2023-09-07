//using Microsoft.EntityFrameworkCore;
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
        public IEnumerable<Recipe> GetAllRecipes()
        {
            return _recipeDbContext.Recipes.ToList();
        }
        public void AddRecipe(Recipe recipe)
        {
            if (recipe == null)
            {
                throw new ArgumentNullException(nameof(recipe));
            }

            _recipeDbContext.Recipes.Add(recipe);
            _recipeDbContext.SaveChanges();
        }

        // Updates an existing recipe.
        public void UpdateRecipe(Recipe recipe)
        {
            //_recipeDbContext.Entry(oldRecipe).State = EntityState.Detached;
            //_recipeDbContext.Update(recipe);
            //_recipeDbContext.Entry(recipe).State = EntityState.Modified;
            //_recipeDbContext.Entry(recipe).State = EntityState.Modified;
            _recipeDbContext.Entry(recipe).State = EntityState.Modified;
            _recipeDbContext.SaveChanges();
        }

        // Deletes a recipe by its unique identifier.
        public void Delete(Recipe recipe)
        {  
            _recipeDbContext.Recipes.Remove(recipe);
            _recipeDbContext.SaveChanges();
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


        public object getUniqueIngredients()
        {
            throw new NotImplementedException();
        }

   
    }
}
