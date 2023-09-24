using RecipeAppDAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppBLL.Services.IService
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
