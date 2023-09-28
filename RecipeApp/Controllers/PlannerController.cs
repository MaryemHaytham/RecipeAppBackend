using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeAppBLL.Services;
using RecipeAppBLL.Services.IService;
using RecipeAppDTO.MealPlanDTO;

namespace RecipeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlannerController : ControllerBase
    {
        IPlannerService _plannerService;
        public PlannerController(IPlannerService plannerService) {
            _plannerService = plannerService;
        }
        [HttpPost]
        public IActionResult CreateMealPlan([FromBody] AddMealPlanDTO createMealPlanDto)
        {
            var mealPlan = _plannerService.CreateMealPlan(createMealPlanDto.DateString, createMealPlanDto.UserID, createMealPlanDto.RecipeID);
            return CreatedAtAction(nameof(GetMealPlanById), new { mealPlanId = mealPlan.MealPlanId }, mealPlan);
        }

        [HttpGet("{mealPlanId}")]
        public IActionResult GetMealPlanById(int mealPlanId)
        {
            var mealPlan = _plannerService.GetMealPlanById(mealPlanId);
            if (mealPlan == null)
            {
                return NotFound();
            }
            return Ok(mealPlan);
        }

        [HttpPut("{mealPlanId}")]
        public IActionResult UpdateMealPlan([FromBody] UpdateMealPlanDTO updateMealPlanDto)
        {
            var mealPlan = _plannerService.UpdateMealPlan(updateMealPlanDto.mealID, updateMealPlanDto.DateString, updateMealPlanDto.RecipeID);
            if (mealPlan == null)
            {
                return NotFound();
            }
            return Ok(mealPlan);
        }
        [HttpDelete("{mealPlanId}")]
        public IActionResult DeleteMealPlan(int mealPlanId)
        {
            _plannerService.DeleteMealPlan(mealPlanId);
            return Ok(new { message = "Plan Deleted Successfully." });
        }
        [HttpGet("GetUserPlansByUserID/{userID}")]
        public IActionResult GetUserMealPlansByUserId(int userID)
        {
            return Ok(_plannerService.GetAllMealPlansForUser(userID));
        }

    }
}
