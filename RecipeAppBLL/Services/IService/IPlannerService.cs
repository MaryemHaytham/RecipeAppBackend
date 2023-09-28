using RecipeAppDAL.Entity.RecipeAppDAL.Entity;
using RecipeAppDTO.MealPlanDTO;
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
        MealPlanDTO CreateMealPlan(string dateString, int userId, int recipeId);

        // Read a meal plan by its ID
        MealPlanDTO GetMealPlanById(int mealPlanId);

        // Update an existing meal plan
        MealPlanDTO UpdateMealPlan(int mealPlanId, string dateString, int recipeId);

        // Delete a meal plan by its ID
        void DeleteMealPlan(int mealPlanId);

        // Get all meal plans for a specific user
        IEnumerable<MealPlanDTO> GetAllMealPlansForUser(int userId);
    }
}
