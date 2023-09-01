using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RecipeAppDAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RecipeAppDAL.DataContext
{
   
    public class RecipeDbContext : DbContext
    {

        public RecipeDbContext(DbContextOptions<RecipeDbContext> options) : base(options) { }

        //recipes table that will be created in db
        public DbSet<Recipe> Recipes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Recipe>().HasData(
                 new Recipe
                 {
                     Id = 2,
                     RecipeName = "Test",
                     Ingredients = "Test ingrrdients",
                     Image = null,
                     Steps = "asdadsdasdsdad",
                     DietaryRestrictions = null

                 }

              ) ;

        }
    }
}
