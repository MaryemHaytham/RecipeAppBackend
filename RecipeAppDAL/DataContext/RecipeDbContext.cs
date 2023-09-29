using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RecipeAppDAL.Entity;
using RecipeAppDAL.Entity.RecipeAppDAL.Entity;
using RecipeAppDAL.Migrations;
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
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<RecipeFavorite> Favorites { get; set; }
        public DbSet<Plans> Plans { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<ShoppingListItem> shoppingListItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction); // Set OnDelete to NoAction

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.User)
                .WithMany(u => u.Ratings)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction); // Set OnDelete to Cascade for the other relationship
            modelBuilder.Entity<RecipeFavorite>()
                .HasKey(rf => new { rf.UserId, rf.RecipeId });

            modelBuilder.Entity<RecipeFavorite>()
                .HasOne(rf => rf.User)
                .WithMany(u => u.FavoriteRecipes)
                .HasForeignKey(rf => rf.UserId);

            modelBuilder.Entity<RecipeFavorite>()
                .HasOne(rf => rf.Recipe)
                .WithMany(r => r.FavoritedByUsers)
                .HasForeignKey(rf => rf.RecipeId);
            modelBuilder.Entity<RecipeFavorite>()
                .HasOne(f => f.User)
                .WithMany(u => u.FavoriteRecipes)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Plans>()
                .HasOne(mp => mp.User)
                .WithMany(u => u.MealPlans)
                .HasForeignKey(mp => mp.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ShoppingListItem>()
                .Property(sli => sli.Id)
                .ValueGeneratedOnAdd();



        }


    }
}
