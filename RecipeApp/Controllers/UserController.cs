using Microsoft.AspNetCore.Mvc;
using RecipeApp.ViewModels;
using RecipeAppBLL.Services.IService;
using RecipeAppDAL.Entity;

namespace RecipeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] AddUserVM user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            var Newuser = new User
            {

                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,

            };
            // Validate and register the user
            bool registrationResult = _userService.RegisterUser(Newuser);


            if (registrationResult)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
    //[HttpPost("login")]



}
