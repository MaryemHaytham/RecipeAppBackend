using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppDAL.Entity
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewId { get; set; }

        [Required]
        [MinLength(1)]
        public string Text { get; set; }

        //[Range(1, 5)] // Assuming a rating scale from 1 to 5
        //public int Rating { get; set; }

        public int UserId { get; set; } // Foreign key to User
        public User User { get; set; } // Navigation property to User

        public int RecipeId { get; set; } // Foreign key to Recipe
        public Recipe Recipe { get; set; } // Navigation property to Recipe

        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
