using RecipeAppDAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppDAL.Repositories.IRepositories 
{
    public interface ICategoriesRepository: IGenericRepository<Categories>
    {
        public Categories findByName(string name);
    }
}
