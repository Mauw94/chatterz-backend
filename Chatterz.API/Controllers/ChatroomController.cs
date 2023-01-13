using Chatterz.API.CachedDb;
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
        private IUsersDbCaching _db;
        
        public ChatroomController(IHubContext<ChatHub> hubContext, IUsersDbCaching db) 
        {
            _hubContext = hubContext;
            _db = db;
        }

        [HttpPost]
        [Route("api/chatroom/create")]
        public async Task<ActionResult<string>> Create(Chatterz.Domain.ConnectionInfo connectionInfo)
        {
            var roomId = Guid.NewGuid().ToString();

            await _hubContext.Groups.AddToGroupAsync(connectionInfo.ConnectionId, roomId);
            await _hubContext.Clients.Groups(roomId).SendAsync("UserConnected", connectionInfo.ConnectionId);

            _db.Save(roomId, connectionInfo.ConnectionId);
            
            return Ok(roomId);
        }

        [HttpGet]
        [Route("api/chatroom/all")]
        public ActionResult<List<string>> GetAllChatrooms()
        {
            var chatrooms = _db.GetAllChatrooms();
            if (chatrooms == null)
                return NotFound($"No chatrooms found to display");

            return Ok(chatrooms);
        }
    }
}