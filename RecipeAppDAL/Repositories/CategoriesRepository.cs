using RecipeAppDAL.DataContext;
using RecipeAppDAL.Entity;
using RecipeAppDAL.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppDAL.Repositories
{
    public class CategoriesRepository : GenericRepository<Categories> ,ICategoriesRepository
    {
        private readonly RecipeDbContext _recipeDbContext;
        public CategoriesRepository(RecipeDbContext recipeDbContext) : base(recipeDbContext)
        {
            _recipeDbContext = recipeDbContext;
        }

        public Categories findByName(string name)
        {
            if(string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            return _recipeDbContext.Categories.FirstOrDefault(c => c.CategoryName == name);
        }
    }
}
