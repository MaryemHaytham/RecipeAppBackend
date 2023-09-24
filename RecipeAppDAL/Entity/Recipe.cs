using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace RecipeAppDAL.Entity
{
    public class Recipe
    {
        public Recipe()
        {
            CreatedOn = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required] //Mandatory
        [MinLength(4)] // Enforce minimum length of 4 characters
        public string RecipeName { get; set; }

        [Required]
        [MinLength(1)] // Enforce minimum length of 1 character
        public string Ingredients { get; set; }

        [Required]
        [MinLength(4)] // Enforce minimum length of 1 character
        public string Steps { get; set; }

        public string ?Image { get; set; }

        public string? DietaryRestrictions { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Popularity { get; set; } = 0;
        public float Rating { get; set; } = 0;
        public int UserId { get; set; } // Foreign key
        public User User { get; set; } // Navigation property
        public int CategoryId { get; set; } //Category foriegn key
        public Categories category { get; set; }
        //public int ReviewId { get; set; }// Foreign key
        public List<Review> Reviews { get; set; }// Navigation property



    }
}
