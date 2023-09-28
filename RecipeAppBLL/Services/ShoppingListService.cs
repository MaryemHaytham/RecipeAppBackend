using RecipeAppBLL.Services.IService;
using RecipeAppBLL.Utilities.CustomExceptions;
using RecipeAppDAL.Entity;
using RecipeAppDAL.Entity.RecipeAppDAL.Entity;
using RecipeAppDAL.Repositories.IRepositories;
using RecipeAppDTO.MealPlanDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppBLL.Services
{
    public class ShoppingListService :IShoppingListService
    {
        IShoppingListRepository _shoppingListRepository;
        IUserRepository _userRepository;
        public ShoppingListService(IShoppingListRepository shoppingListRepository) { 
            this._shoppingListRepository = shoppingListRepository;
        }
        public void GenerateShoppingListFromMealPlans(int userId,MealPlanDTO plan)
        {
            // Retrieve meal plans for the user and add the ingredients to the shopping list.
            // You will need to implement this logic based on your data structure.
            // You can iterate through the user's meal plans, get the recipes, and extract ingredients.
        }

        public void AddItemToShoppingList(int userId, string itemName, string category = null)
        {
           // if()
            //_shoppingListRepository.Add()
        }

        public void GetShoppingListByUserID(int userId)
        {
            User user = _userRepository.GetById(userId);
            if(user == null)
            {
                throw new CustomException("User not found");
            }
            int shoppingListID;

        }

        public void MarkItemAsPurchased(int userId, int itemId)
        {
            // Retrieve the item from the user's shopping list and mark it as purchased.
            // Save the changes to the repository.
        }

        //public List<ShoppingListItem> GetCategorizedShoppingList(int userId)
        //{
            // Retrieve the user's shopping list items and group them by category.
            // Return the categorized shopping list.
        //}


    }
}
