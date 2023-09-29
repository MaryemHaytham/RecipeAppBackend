using Microsoft.Identity.Client;
using RecipeAppBLL.Services.IService;
using RecipeAppBLL.Utilities.CustomExceptions;
using RecipeAppDAL.Entity;
using RecipeAppDAL.Entity.RecipeAppDAL.Entity;
using RecipeAppDAL.Migrations;
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
        readonly IShoppingListRepository _shoppingListRepository;
        readonly IUserRepository _userRepository;
        readonly IGenericRepository<ShoppingListItem> _genericRepository;
        readonly IPlannerRepository _plannerRepository;
        readonly IRecipeRepository _recipeRepository;
        public ShoppingListService(IShoppingListRepository shoppingListRepository, IGenericRepository<ShoppingListItem> genericRepository, IUserRepository userRepository,IPlannerRepository plannerRepository,IRecipeRepository recipeRepository) { 
            this._shoppingListRepository = shoppingListRepository;
            this._genericRepository = genericRepository;
            this._userRepository = userRepository;
            this._plannerRepository = plannerRepository;
            this._recipeRepository = recipeRepository;
        }
        // Fetches a user by ID and throws an exception if the user doesn't exist.
        private User GetUserById(int userId)
        {
            User user = _userRepository.GetById(userId);
            if (user == null)
            {
                throw new CustomException("User does not exist");
            }
            return user;
        }
        // Creates and initializes a new ShoppingList for a user.
        private ShoppingList CreateShoppingList(User user)
        {
            if (GetShoppingListByUserID(user.UserId) != null)
            {
                return user.ShoppingList; // Return the existing shopping list
            }
            var shoppingList = new ShoppingList();
            shoppingList.User = user;
            shoppingList.UserId = user.UserId; 
            user.ShoppingList = shoppingList; // Associate the user with the shopping list.
            _shoppingListRepository.Add(shoppingList);
            _userRepository.Update(user);
            
            return shoppingList;
        }
        // Adds ingredients from meal plans to the shopping list.
        private void AddIngredientsToShoppingList(ShoppingList shoppingList, IEnumerable<Plans> mealPlans)
        {
            var shoppingListItems = new List<ShoppingListItem>();

            foreach (var mealPlan in mealPlans)
            {
                int recipeId = mealPlan.RecipeId;
                Recipe recipe = _recipeRepository.GetById(recipeId);

                if (recipe == null)
                {
                    throw new CustomException("Recipe is null");
                }

                var ingredients = recipe.Ingredients.Split(',').Select(i => i.Trim());

                foreach (var ingredient in ingredients)
                {
                    var shoppingListItem = new ShoppingListItem();
                    shoppingListItem.Name = ingredient;
                    shoppingListItem.ShoppingList = shoppingList;
                    shoppingListItem.ShoppingListId = shoppingList.Id;
                    shoppingListItems.Add(shoppingListItem);
                }
            }

            shoppingList.Items = shoppingListItems;
            _genericRepository.AddRange(shoppingListItems);
            _shoppingListRepository.Update(shoppingList);
        }
        public void GenerateShoppingListFromMealPlans(int userId)
        {
            // Fetch the user by ID.
            User user = GetUserById(userId);

            // Create and initialize a new ShoppingList.
            ShoppingList shoppingList = CreateShoppingList(user);

            // Get meal plans for the user.
            var mealPlans = _plannerRepository.GetPlansForUser(userId);

            // Add ingredients from meal plans to the shopping list.
            AddIngredientsToShoppingList(shoppingList, mealPlans);

            // Update the shopping list and associated items.
            _shoppingListRepository.Update(shoppingList);
        }

        public ShoppingList AddItemToShoppingListManually(int userId, string itemName, string category = null)
        {
            // Fetch the user by ID.
            User user = GetUserById(userId);

            // Create and initialize a new ShoppingList.
            ShoppingList shoppingList = CreateShoppingList(user);

            //Create Item
            ShoppingListItem shoppingListItem = new ShoppingListItem();
            shoppingListItem.Name = itemName;
            shoppingListItem.Category = category;
            shoppingListItem.ShoppingListId=shoppingList.Id;
            shoppingList.Items.Add(shoppingListItem);

            //Add item to db
            _genericRepository.Update(shoppingListItem);

            //Update Shopping List
            
            _shoppingListRepository.Update(shoppingList) ;

            return shoppingList;
        }
        public IEnumerable<ShoppingListItem> GetItems(int userId)
        {
            User user = GetUserById(userId);

            // Create and initialize a new ShoppingList.
            ShoppingList shoppingList = CreateShoppingList(user);
            
            return shoppingList.Items;
        }

        public ShoppingList GetShoppingListByUserID(int userId)
        {
            User user = _userRepository.GetById(userId);
            if(user == null)
            {
                throw new CustomException("User not found");
            }
            ShoppingList shoppingList = _shoppingListRepository.GetShoppingListByUserID(userId);
            
            return shoppingList;
        }

        public void MarkItemAsPurchased(int userId, int itemId)
        {
            // Fetch the user by ID.
            User user = GetUserById(userId);
            ShoppingListItem item= _genericRepository.GetById(itemId);
            item.IsPurchased = true;
            _genericRepository.Update(item);

        }

        //public List<ShoppingListItem> GetCategorizedShoppingList(int userId)
        //{
            // Retrieve the user's shopping list items and group them by category.
            // Return the categorized shopping list.
        //}


    }
}
