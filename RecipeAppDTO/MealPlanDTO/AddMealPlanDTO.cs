using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppDTO.MealPlanDTO
{
    public class AddMealPlanDTO
    {
        public string DateString { get; set; }
        public int RecipeID { get; set; }
        public int UserID { get; set; }
    }
}
