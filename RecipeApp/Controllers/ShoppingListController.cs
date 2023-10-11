using Microsoft.AspNetCore.Mvc;
using RecipeAppBLL.Services.IService;
using RecipeAppBLL.Utilities.CustomExceptions;
using RecipeAppDAL.Entity;

namespace RecipeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingListController :ControllerBase
    {
        private readonly IShoppingListService _shoppingListService;

        public ShoppingListController(IShoppingListService shoppingListService)
        {
            _shoppingListService = shoppingListService;
        }

        [HttpGet("GetByUserID/{userId}")]
        public IActionResult GetByUserID(int userId)
        {
           
            var shoppingList = _shoppingListService.GetShoppingListByUserID(userId);
            return Ok(shoppingList);
           
        }

        [HttpPost("GenerateShoppingList/{userId}")]
        public IActionResult GenerateShoppingList(int userId)
        {

            _shoppingListService.GenerateShoppingListFromMealPlans(userId);
            return Ok(new {Message = "Shopping list generated successfully."});
            
        }

        [HttpPut("MarkAsDone/{userId}/{itemId}")]
        public IActionResult MarkItemAsPurchased(int userId, int itemId)
        {
            _shoppingListService.MarkItemAsPurchased(userId, itemId);
            return Ok();
        }
        [HttpPost("AddItemManually/{userId}/{itemName}")]
        public IActionResult AddItemToShoppingListManually(int userId, string itemName, string? category = null)
        {
            return Ok(_shoppingListService.AddItemToShoppingListManually(userId, itemName, category));
        }
        [HttpGet("GetItems/{userId}")]
        public IActionResult getItems(int userId)
        {
            return Ok(_shoppingListService.GetItems(userId));
        }
    }
}
