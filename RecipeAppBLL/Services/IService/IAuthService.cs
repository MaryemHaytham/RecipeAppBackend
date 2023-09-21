using RecipeAppDAL.Entity;

namespace RecipeAppBLL.Services.IService
{
    public interface IAuthService
    {
        string GenerateTokenString(User user);
        Task<bool> Login(User user);
        Task<bool> RegisterUser(User user);
    }
}