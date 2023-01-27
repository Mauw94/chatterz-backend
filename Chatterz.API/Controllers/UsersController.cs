using Chatterz.Domain;
using Chatterz.Domain.DTO;
using Chatterz.Domain.Enums;
using Chatterz.Domain.Models;
using Chatterz.HUBS;
using Chatterz.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chatterz.API.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IHubContext<ChatHub> _hubContext;
        private IUserService _userService;

        public UsersController(IHubContext<ChatHub> hubContext, IUserService userService)
        {
            _hubContext = hubContext;
            _userService = userService;
        }

        [HttpPost]
        [Route("api/users/create")]
        public async Task<ActionResult> Add(string username, string password)
        {
            var user = new User()
            {
                UserName = username,
                Password = password
            };

            await _userService.AddAsync(user);
            return Ok();
        }

        [HttpPost]
        [Route("api/users/change_username")]
        public async Task<ActionResult> ChangeUsername(ChangeUsernameDto dto)
        {
            var user = await _userService.GetAsync(dto.UserId);
            user.UserName = dto.NewUsername;

            await _hubContext.Clients.Group(dto.ChatroomId.ToString())
                .SendAsync("UsernameUpdated", dto);

            return Ok();
        }

        [HttpGet]
        [Route("api/users/all")]
        public async Task<ActionResult<List<User>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("api/users/challenge")]
        public async Task<ActionResult> Challenge(int challengerUserId, int userId, string inviteMessage, int gameType)
        {
            var user = await _userService.GetAsync(userId);
            var challenger = await _userService.GetAsync(challengerUserId);

            var gameInviteDto = new GameInviteDto() // TODO: this dto needs rework
            {
                Challenger = challenger,
                InviteMessage = challenger.UserName + " " + inviteMessage,
                GameType = gameType,
                UserId = userId
            };

            await _hubContext.Clients.Client(user.ConnectionId)
                .SendAsync("GameInvite", gameInviteDto);

            return Ok();
        }

        [HttpPost]
        [Route("api/users/accept_gameinvite")]
        public async Task<ActionResult> AcceptGameInvite(GameInviteDto gameInvite)
        {
            var user = await _userService.GetAsync(gameInvite.UserId);
            var challenger = await _userService.GetAsync(gameInvite.Challenger.Id);

            await _hubContext.Clients
                .Clients(challenger.ConnectionId, user.ConnectionId)
                .SendAsync("AcceptGameInvite", gameInvite.GameType);

            return Ok();
        }
    }
}