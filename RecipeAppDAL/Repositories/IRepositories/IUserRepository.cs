using RecipeAppDAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppDAL.Repositories.IRepositories
{
    public interface IUserRepository
    {
        User GetUserById(int userId);
        User GetUserByEmail(string email);
        void Add(User user);
        void UpdateUser(User user);
        IEnumerable<User> GetAll();
    }
}
