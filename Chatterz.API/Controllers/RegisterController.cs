using Chatterz.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Chatterz.Services.Interfaces;
using Chatterz.Domain.DTO;

namespace Chatterz.API.Controllers
{
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserService _userService;

        public RegisterController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("api/register/register")]
        public async Task<ActionResult<User>> Register(UserLoginDto dto)
        {
            var user = new User()
            {
                UserName = dto.UserName,
                Password = dto.Password
            };

            await _userService.AddAsync(user);

            return Ok(user);
        }
    }
}