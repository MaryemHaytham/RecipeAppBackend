using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeApp.ViewModels;
using RecipeAppBLL.Services.IService;
using RecipeAppDAL.Entity;
using RecipeAppDAL.Repositories.IRepositories;

namespace RecipeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _iJwtService;

        public UserController(IUserService userService, IUserRepository userRepository, IJwtService ijwtService)
        {
            _userService = userService;
            _userRepository = userRepository;
            _iJwtService = ijwtService;

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
        //[HttpPost("login")]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUserVM request)
        {
            var user = _userRepository.GetUserByEmail(request.Email);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            // Hash the password entered during login
            string hashedPassword = _userService.HashPassword(request.Password);

            if (user.Password != hashedPassword)
            {
                return BadRequest("Invalid Password");
            }

            var token = _iJwtService.GenerateToken(user);

            return Ok(new { Token = token });
        }
        //[Authorize] //this attribute to secure the endpoint
        //[HttpGet("secure-endpoint")]
        //public IActionResult SecureEndpoint()
        //{

        //    return Ok("This is a secure endpoint.");
        //}
        [HttpGet("GetUser/{id}")]
        public IActionResult GetUser(int id)
        {
            return Ok(_userService.GetUser(id));
        }

    }
    


}
