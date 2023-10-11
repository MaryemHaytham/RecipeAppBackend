using Microsoft.EntityFrameworkCore;
using RecipeAppDAL.DataContext;
using RecipeAppDAL.Entity;
using RecipeAppDAL.Entity.RecipeAppDAL.Entity;
using RecipeAppDAL.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppDAL.Repositories
{
    public class ShoppingListRepository : GenericRepository<ShoppingList>, IShoppingListRepository
    {
        private readonly DataContext.RecipeDbContext _recipeDbContext;
        public ShoppingListRepository(RecipeDbContext recipeDbContext) : base(recipeDbContext) {
            _recipeDbContext = recipeDbContext;
        }

        public ShoppingList GetShoppingListByUserID(int userId)
        {
            Expression<Func<ShoppingList, bool>> filter = sl => sl.UserId == userId;
            Expression<Func<ShoppingList, object>>[] includes = { sl => sl.Items };

            return Get(filter, includes).FirstOrDefault();
        }
    }
}
