using Chatterz.API.InMemoryDb;
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
        private IChatroomDb _db;

        public ChatroomController(IHubContext<ChatHub> hubContext, IChatroomDb db)
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

            if (!_db.SaveChatroom(roomId, connectionInfo.ConnectionId))
                return BadRequest("Something went wrong creating and savint the chatroom.");

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

        [HttpPost]
        [Route("api/chatroom/send")]
        public ActionResult Send(string chatroomId, ChatMessage chatMessage)
        {
            if (!_db.SaveChat(chatroomId, chatMessage))
                return BadRequest($"Couldn't save chatroom {chatroomId}");

            return Ok();
        }

        [HttpGet]
        [Route("api/chatroom/history")]
        public ActionResult<List<string>> GetChatHistory(string chatroomId)
        {
            return Ok(_db.GetChatHistory(chatroomId));
        }
    }
}