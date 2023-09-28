using RecipeAppDAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppDAL.Repositories.IRepositories
{
    public interface IShoppingListRepository :IGenericRepository<ShoppingList>
    {
        ShoppingList GetShoppingListByUserID(int userId);
    }
}
