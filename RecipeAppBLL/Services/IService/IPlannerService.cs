using RecipeAppDAL.Entity.RecipeAppDAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppBLL.Services.IService
{
    public interface IPlannerService
    {
        // Create a new meal plan
        MealPlan CreateMealPlan(DateTime date, int userId, int recipeId);

        // Read a meal plan by its ID
        MealPlan GetMealPlanById(int mealPlanId);

        // Update an existing meal plan
        MealPlan UpdateMealPlan(int mealPlanId,DateTime date, int userId, int recipeId);

        // Delete a meal plan by its ID
        void DeleteMealPlan(int mealPlanId);

        // Get all meal plans for a specific user
        List<MealPlan> GetAllMealPlansForUser(int userId);
    }
}
