using RecipeAppDAL.Entity;
using RecipeAppDTO.ShoppingListDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppBLL.Services.IService
{
    public interface IShoppingListService
    {
        public void GenerateShoppingListFromMealPlans(int userId);
        public ShoppingListDTO GetShoppingListByUserID(int userId);
        public void MarkItemAsPurchased(int userId, int itemId);
        public ShoppingList AddItemToShoppingListManually(int userId, string itemName, string category = null);
        public IEnumerable<ShoppingListItem> GetItems(int userId);

    }
}
