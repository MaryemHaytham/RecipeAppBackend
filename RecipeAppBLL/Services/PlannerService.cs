using AutoMapper;
using RecipeAppBLL.Services.IService;
using RecipeAppBLL.Utilities.CustomExceptions;
using RecipeAppBLL.Utilities.Validators.IValidators;
using RecipeAppDAL.Entity;
using RecipeAppDAL.Entity.RecipeAppDAL.Entity;
using RecipeAppDAL.Repositories;
using RecipeAppDAL.Repositories.IRepositories;
using RecipeAppDTO.MealPlanDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RecipeAppBLL.Services
{
    public class PlannerService : IPlannerService
    {
        readonly IPlannerRepository _plannerRepository;
        readonly IRecipeValidator _recipeValidator;
        readonly IMapper _mapper;
        readonly IRecipeRepository _recipeRepository;
        public PlannerService(IPlannerRepository plannerRepository,IRecipeValidator recipeValidator,IMapper mapper, IRecipeRepository recipeRepository) { 
            this._plannerRepository = plannerRepository;
            this._recipeValidator = recipeValidator;
            this._mapper = mapper;
            this._recipeRepository = recipeRepository;
        }
        public Plans validatePlan(int mealPlanId)
        {
            Plans mealPlan = _plannerRepository.GetById(mealPlanId);
            if (mealPlan == null)
            {
                throw new CustomException("Meal plan not found");
            }
            return mealPlan;
        }
        public MealPlanDTO CreateMealPlan(string dateString, int userId, int recipeId)
        {
            var mealPlan = new Plans();
            DateTime date = DateTime.Parse(dateString,
                          System.Globalization.CultureInfo.InvariantCulture);
            mealPlan.Date = date;
            mealPlan.UserId = userId;
            mealPlan.RecipeId = recipeId;
            Recipe recipe = _recipeValidator.validateRecipe(recipeId);
            if(recipe == null)
            {
                throw new CustomException("Recipe Not Found");
            }
            mealPlan.Recipe=recipe;
            _plannerRepository.Add(mealPlan);

            return _mapper.Map<MealPlanDTO>(mealPlan);
        }

        public void DeleteMealPlan(int mealPlanId)
        {
            Plans mealPlan= validatePlan(mealPlanId);
            _plannerRepository.Delete(mealPlan);
        }

        public IEnumerable<MealPlanDTO> GetAllMealPlansForUser(int userId)
        {
            IEnumerable<Plans> plans = _plannerRepository.GetPlansForUser(userId);
            IEnumerable<MealPlanDTO> plansDTO = _mapper.Map<IEnumerable<MealPlanDTO>>(plans);
            foreach (MealPlanDTO plan in plansDTO)
            {
                Recipe recipe = _recipeRepository.GetById(plan.RecipeId);
                plan.MealName = recipe.RecipeName;
            }
            return plansDTO;
        }

        public MealPlanDTO GetMealPlanById(int mealPlanId)
        {
            Plans mealPlan = validatePlan(mealPlanId);
            MealPlanDTO planDTO = _mapper.Map<MealPlanDTO>(mealPlan);
            Recipe recipe = _recipeRepository.GetById(planDTO.RecipeId);
            planDTO.MealName = recipe.RecipeName;

            return _mapper.Map<MealPlanDTO>(planDTO);
        }

        public MealPlanDTO UpdateMealPlan(int mealPlanId, string dateString, int recipeId)
        {
            Recipe recipe = _recipeValidator.validateRecipe(recipeId);
            Plans mealPlan = validatePlan(mealPlanId);
            mealPlan.Recipe=recipe;
            mealPlan.Date =DateTime.Parse(dateString,
                          System.Globalization.CultureInfo.InvariantCulture);
            mealPlan.RecipeId=recipeId;
            _plannerRepository.Update(mealPlan);
            return _mapper.Map<MealPlanDTO>(mealPlan);


        }
    }
}
