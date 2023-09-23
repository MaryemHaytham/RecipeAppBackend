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
        public DbSet<Ingredients> Ingredients { get; set; }
        public DbSet<Categories> Categories { get; set; }
        

    }
}
