using Chatterz.API.InMemoryDb;
using Chatterz.Domain;
using Chatterz.Domain.DTO;
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
        private IUsersDb _usersDb;

        public ChatroomController(
            IHubContext<ChatHub> hubContext,
            IChatroomDb db,
            IUsersDb usersDb)
        {
            _hubContext = hubContext;
            _db = db;
            _usersDb = usersDb;
        }

        [HttpPost]
        [Route("api/chatroom/create")]
        public async Task<ActionResult<string>> Create(Chatterz.Domain.ConnectionInfo connectionInfo)
        {
            var roomId = Guid.NewGuid().ToString();

            await _hubContext.Groups.AddToGroupAsync(connectionInfo.ConnectionId, roomId);
            await _hubContext.Clients.Groups(roomId).SendAsync("UserConnected", connectionInfo.ConnectionId);

            if (!_db.SaveChatroom(roomId, connectionInfo.UserId))
                return BadRequest("Something went wrong creating and savint the chatroom.");

            return Ok(roomId);
        }

        [HttpGet]
        [Route("api/chatroom/all")]
        public ActionResult<List<ChatroomDto>> GetAllChatrooms()
        {
            var chatrooms = _db.GetAllChatrooms();
            if (chatrooms == null)
                return Ok(null);

            var dtos = new List<ChatroomDto>();

            foreach (var chatroom in chatrooms)
            {
                var users = new List<User>();

                if (chatroom.Value.Any())
                {
                    foreach (var userId in chatroom.Value)
                    {
                        users.Add(_usersDb.GetUser(userId));
                    }
                }

                dtos.Add
                (
                    new ChatroomDto
                    {
                        Id = chatroom.Key,
                        Users = users
                    }
                );
            }

            return Ok(dtos);
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