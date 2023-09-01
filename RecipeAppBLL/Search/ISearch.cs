using RecipeAppDAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeAppDAL.Repositories.IRepositories;

namespace RecipeAppBLL.Search
{
    internal interface ISearch
    {
        public IEnumerable<Recipe> Search(string searchCategory, IRecipeFilteringRepository _recipeRepository);
    }
}
