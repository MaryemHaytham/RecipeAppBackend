using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppDTO.MealPlanDTO
{
    public class UpdateMealPlanDTO
    {
        public int mealID { get; set; }
        public string DateString { get; set; }
        public int RecipeID { get; set; }

    }
}
