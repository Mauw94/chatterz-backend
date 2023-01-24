using Chatterz.Domain.DTO;
using Chatterz.HUBS;
using Chatterz.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chatterz.API.Controllers
{
    [ApiController]
    public class SignalRController : ControllerBase
    {
        private IHubContext<ChatHub> _hubContext;
        private readonly IUserService _userService;
        private readonly IChatroomService _chatroomService;

        public SignalRController(
            IHubContext<ChatHub> hubContext,
            IUserService userService,
            IChatroomService chatroomService
            )
        {
            _hubContext = hubContext;
            _userService = userService;
            _chatroomService = chatroomService;
        }

        [HttpPost]
        [Route("api/signalr/connect")]
        public async Task<ActionResult> Connect(ChatroomJoinDto dto)
        {
            var user = await _userService.GetAsync(dto.UserId);
            var chatroom = await _chatroomService.GetAsync(dto.ChatroomId);
            var oldchatroomId = user.ChatroomId.ToString();
            chatroom.Users.Add(user);

            if (oldchatroomId != null)
                await _hubContext.Groups.RemoveFromGroupAsync(dto.ConnectionId, oldchatroomId);

            await _hubContext.Groups.AddToGroupAsync(dto.ConnectionId, dto.ChatroomId.ToString());
            await _hubContext.Clients.Group(dto.ChatroomId.ToString())
                .SendAsync("UserConnected", dto.ConnectionId);

            return Ok();
        }

        [HttpGet]
        [Route("api/signalr/update")]
        public async Task<ActionResult> UpdateConnectionId(int userId, string connectionId)
        {
            var user = await _userService.GetAsync(userId);
            user.ConnectionId = connectionId;

            return Ok();
        }

        [HttpPost]
        [Route("api/signalr/disconnect")]
        public ActionResult Disconnect()
        {
            return Ok();
        }
    }
}
