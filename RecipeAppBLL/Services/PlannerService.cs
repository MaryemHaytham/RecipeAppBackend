using RecipeAppBLL.Services.IService;
using RecipeAppDAL.Entity.RecipeAppDAL.Entity;
using RecipeAppDAL.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppBLL.Services
{
    public class PlannerService : IPlannerService
    {
        readonly IPlannerRepository _plannerRepository;
        readonly IRecipeRepository _recipeRepository;
        public PlannerService(IPlannerRepository plannerRepository) { 
            this._plannerRepository = plannerRepository;
        }
        public MealPlan CreateMealPlan(DateTime date, int userId, int recipeId)
        {
            var mealPlan = new MealPlan();
            mealPlan.Date = date;
            mealPlan.UserId = userId;
            mealPlan.RecipeId = recipeId;
            mealPlan.Recipe=_recipeRepository.GetById(recipeId);
            _plannerRepository.Add(mealPlan);
            return mealPlan;
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
