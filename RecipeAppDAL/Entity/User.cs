using RecipeAppDAL.Entity.RecipeAppDAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppDAL.Entity
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<RecipeFavorite> FavoriteRecipes { get; set; }
        public List<Recipe> Recipes { get; set; } // Navigation property
        public List<Review> Reviews { get; set; }// Navigation property
        public List<Rating> Ratings { get; set; }// Navigation property
        public List<Plans> MealPlans { get; set; }

        // Reference property to ShoppingList (one-to-one relationship)
        public ShoppingList ShoppingList { get; set; }


    }
}
