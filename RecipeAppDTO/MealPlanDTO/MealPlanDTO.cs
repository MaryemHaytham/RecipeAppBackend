using RecipeAppDAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppDTO.MealPlanDTO
{
    public class MealPlanDTO
    {
        public int MealPlanId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        // Foreign key to User
        public int UserId { get; set; }

        // Foreign key to Recipe
        public int RecipeId { get; set; }

    }
}
