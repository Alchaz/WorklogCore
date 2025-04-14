using Entities;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

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
        public async Task<IActionResult> Login(string userName)
        {
            User user = await _userService.LoginUser(userName);
            if (user != null)
            {
               var token = Authentication.GenerateJwtToken(user.Id,userName, user.Role);
               return Ok(new { Token = token, User = user });
            }
            else
            {
                return Unauthorized("Invalid credentials.");
            }
        }

    }
}
