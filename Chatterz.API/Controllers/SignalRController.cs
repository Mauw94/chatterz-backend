using Chatterz.API.InMemoryDb;
using Chatterz.Domain.DTO;
using Chatterz.HUBS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chatterz.API.Controllers
{
    [ApiController]
    public class SignalRController : ControllerBase
    {
        private IHubContext<ChatHub> _hubContext;
        private IChatroomDb _chatroomDb;
        private IUsersDb _usersDb;

        public SignalRController(IHubContext<ChatHub> hubContext, IChatroomDb chatroomDb, IUsersDb usersDb)
        {
            _hubContext = hubContext;
            _chatroomDb = chatroomDb;
            _usersDb = usersDb;
        }

        [HttpPost]
        [Route("api/signalr/connect")]
        public async Task<ActionResult> Connect(ChatroomJoinDto dto)
        {
            var oldchatroomId = _chatroomDb.Join(dto.ChatroomId, dto.UserId);

            if (oldchatroomId != null)
                await _hubContext.Groups.RemoveFromGroupAsync(dto.ConnectionId, oldchatroomId);

            await _hubContext.Groups.AddToGroupAsync(dto.ConnectionId, dto.ChatroomId);
            await _hubContext.Clients.Group(dto.ChatroomId).SendAsync("UserConnected", dto.ConnectionId);


            return Ok();
        }

        [HttpGet]
        [Route("api/signalr/update")]
        public ActionResult UpdateConnectionId(string userId, string connectionId)
        {
            var user = _usersDb.GetUser(userId);
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
