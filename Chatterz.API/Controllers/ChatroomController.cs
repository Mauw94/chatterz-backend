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
            _db.SaveChatroom(roomId);
            await _hubContext.Clients.All.SendAsync("RoomsUpdated", GetAllChatrooms());

            return Ok(roomId);
        }

        [HttpPost]
        [Route("api/chatroom/join")]
        public async Task<ActionResult> Join(ChatroomJoinDto dto)
        {
            var oldChatroomId = _db.Join(dto.ChatroomId, dto.UserId);

            if (oldChatroomId != null)
                await _hubContext.Groups.RemoveFromGroupAsync(dto.ConnectionId, oldChatroomId);

            await _hubContext.Groups.AddToGroupAsync(dto.ConnectionId, dto.ChatroomId);
            await _hubContext.Clients.Group(dto.ChatroomId).SendAsync("UserConnected", dto.ConnectionId);

            return Ok();
        }

        [HttpGet]
        [Route("api/chatroom/all")]
        public ActionResult<List<ChatroomDto>> GetAll()
        {
            var chatrooms = GetAllChatrooms();

            return Ok(chatrooms);
        }

        [HttpPost]
        [Route("api/chatroom/send")]
        public ActionResult Send(ChatMessage chatMessage)
        {
            if (!_db.SaveChat(chatMessage))
                return BadRequest($"Couldn't save chatroom {chatMessage.ChatroomId}");

            return Ok();
        }

        [HttpGet]
        [Route("api/chatroom/history")]
        public ActionResult<List<ChatMessage>> GetChatHistory(string chatroomId)
        {
            return Ok(_db.GetChatHistory(chatroomId));
        }

        private List<ChatroomDto> GetAllChatrooms()
        {
            var chatrooms = _db.GetAllChatrooms();
            if (chatrooms == null)
                return null;

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

            return dtos;
        }
    }
}