using Chatterz.Domain;
using Chatterz.HUBS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chatterz.API.Controllers
{
    [ApiController]
    public class ChatroomController : ControllerBase 
    {
        private IHubContext<ChatHub> _hubContext;

        public ChatroomController(IHubContext<ChatHub> hubContext) 
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        [Route("api/chatroom/create")]
        public async Task<string> Create(Chatterz.Domain.ConnectionInfo connectionInfo)
        {
            var roomId = Guid.NewGuid().ToString();

            await _hubContext.Groups.AddToGroupAsync(connectionInfo.ConnectionId, roomId);
            await _hubContext.Clients.Groups(roomId).SendAsync("UserConnected", connectionInfo.ConnectionId);
            
            return roomId;
        }
    }
}