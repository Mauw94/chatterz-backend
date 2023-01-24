using Chatterz.Domain.Models;
using Chatterz.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Chatterz.Services.Interfaces;

namespace Chatterz.API.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("api/login/login")]
        public async Task<ActionResult<User>> Login(UserLoginDto userLogin)
        {
            var user = await _userService.Login(userLogin.UserName, userLogin.Password);
            if (user == null)
                return BadRequest("Login credentials do not match.");

            return Ok(user);
        }

        [HttpPost]
        [Route("api/login/create")]
        public async Task<ActionResult<int>> CreateTempUser(UserLoginDto userLogin)
        {
            var user = new User();
            user.UserName = userLogin.UserName;
            user.Password = userLogin.Password;

            await _userService.AddAsync(user);

            return Ok(user.Id);
        }
    }
}