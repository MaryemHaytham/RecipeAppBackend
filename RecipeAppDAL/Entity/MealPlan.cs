using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppDAL.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace RecipeAppDAL.Entity
    {
        public class MealPlan
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int MealPlanId { get; set; }

            [Required]
            public DateTime Date { get; set; }

            // Foreign key to User
            public int UserId { get; set; }

            // Navigation property to User (many-to-one relationship)
            public User User { get; set; }

            // Foreign key to Recipe
            public int RecipeId { get; set; }

            // Navigation property to Recipe (one-to-one relationship)
            public Recipe Recipe { get; set; }
        }
    }

}
