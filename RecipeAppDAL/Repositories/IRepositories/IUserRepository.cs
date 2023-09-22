using RecipeAppDAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppDAL.Repositories.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User GetUserByEmail(string email);
    }
}
