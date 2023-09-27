using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppDAL.Entity
{
    public class Rating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RatingId { get; set; }

        [Range(1, 5)] // Assuming a rating scale from 1 to 5
        public int Value { get; set; }

        public int UserId { get; set; } // Foreign key to User
        public User User { get; set; } // Navigation property to User

        public int Id { get; set; } // Foreign key to Recipe
        public Recipe Recipe { get; set; } // Navigation property to Recipe
    }
}
