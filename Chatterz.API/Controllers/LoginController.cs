using Chatterz.Domain.Models;
using Chatterz.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Chatterz.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Chatterz.HUBS;
using Chatterz.API.Manages.Interfaces;

namespace Chatterz.API.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ISignalRManager _signalRManager;


        public LoginController(
            IUserService userService,
            ISignalRManager signalRManager)
        {
            _userService = userService;
            _signalRManager = signalRManager;
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

        [HttpPost]
        [Route("api/login/logout")]
        public async Task<ActionResult> Logout(int userId, string connectionId)
        {
            var user = await _userService.GetAsync(userId);

            if (user.ChatroomId.HasValue)
                await _signalRManager.UpdateChatroomsOnUserLeave(user, user.ChatroomId.Value, connectionId);

            user = await _userService.Logout(userId);

            return Ok();
        }
    }
}