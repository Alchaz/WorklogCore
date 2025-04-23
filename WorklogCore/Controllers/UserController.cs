using Entities;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using WorklogCore.Models;

namespace WorklogCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController  : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            User user = await _userService.LoginUser(loginModel.User);
            if (user != null)
            {
               var token = Authentication.GenerateJwtToken(user.Id, loginModel.User, user.Role);
               return Ok(new { Token = token, User = user });
            }
            else
            {
                return Unauthorized("Invalid credentials.");
            }
        }

    }
}
