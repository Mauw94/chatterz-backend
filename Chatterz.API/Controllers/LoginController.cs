using Chatterz.Domain.Models;
using Chatterz.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Chatterz.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Chatterz.HUBS;

namespace Chatterz.API.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IChatroomService _chatroomService;
        private readonly IHubContext<ChatHub> _hubContext;

        public LoginController(
            IUserService userService,
            IChatroomService chatroomService,
            IHubContext<ChatHub> hubContext)
        {
            _userService = userService;
            _hubContext = hubContext;
            _chatroomService = chatroomService;
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
        public async Task<ActionResult> Logout(int userId, string connectionId, int chatroomId)
        {
            var user = await _userService.Logout(userId);

            // TODO: actually should only have to update 1 chatroom here and not retrieve all again.
            var chatroom = await _chatroomService.GetAsync(chatroomId);
            var allChatrooms = await _chatroomService.GetAllWithUsers();

            await _hubContext.Clients.Group(chatroomId.ToString())
                .SendAsync("UserDisconnected", user.UserName);
            await _hubContext.Groups
                .RemoveFromGroupAsync(connectionId, chatroomId.ToString());
            await _hubContext.Clients.Group(chatroomId.ToString())
                .SendAsync("UpdateUsersList", chatroom.Users);
            await _hubContext.Clients.All
                .SendAsync("RoomsUpdated", allChatrooms);

            return Ok();
        }
    }
}