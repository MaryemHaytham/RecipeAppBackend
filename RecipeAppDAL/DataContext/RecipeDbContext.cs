using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RecipeAppDAL.Entity;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
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
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 

            
            modelBuilder.Entity<Recipe>().HasData(
                new Recipe
                {
                    Id = 1,
                    RecipeName = "Test",
                    Ingredients="i1,i2,i3",
                    Image = null,
                    Steps = "s1,s2,s3,s4",
                    DietaryRestrictions = null
                }
            );


            base.OnModelCreating(modelBuilder);
        }

    }
}
