using RecipeAppBLL.Services.IService;
using RecipeAppDAL.Entity.RecipeAppDAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppBLL
{
    public class PlannerService : IPlannerService
    {
        public MealPlan CreateMealPlan(DateTime date, int userId, int recipeId)
        {
            throw new NotImplementedException();
        }

        public void DeleteMealPlan(int mealPlanId)
        {
            throw new NotImplementedException();
        }

        public List<MealPlan> GetAllMealPlansForUser(int userId)
        {
            throw new NotImplementedException();
        }

        public MealPlan GetMealPlanById(int mealPlanId)
        {
            throw new NotImplementedException();
        }

        public MealPlan UpdateMealPlan(int mealPlanId, DateTime date, int userId, int recipeId)
        {
            throw new NotImplementedException();
        }
    }
}
