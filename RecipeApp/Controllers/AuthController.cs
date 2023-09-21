using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeAppBLL.Services.IService;
using RecipeAppDAL.Entity;

namespace RecipeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(User userName)

        {
            if(await _authService.RegisterUser(userName))
            {
                return Ok("Done");
            }
            return BadRequest();
            


        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(User user)
        {
            //await _authService.Login(user);
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            if(await _authService.Login(user))
            {
                var tokenString = _authService.GenerateTokenString(user);
                return Ok(tokenString);
            }
            return BadRequest();

        }

    }
}
