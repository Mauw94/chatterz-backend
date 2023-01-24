using Chatterz.Domain.Models;
using Chatterz.Domain.DTO;
using Chatterz.HUBS;
using Chatterz.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Chatterz.API.Controllers
{
    [ApiController]
    public class ChatroomController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IUserService _userService;
        private readonly IChatroomService _chatroomService;
        private readonly IService<ChatMessage> _chatMessageService;


        public ChatroomController(
            IHubContext<ChatHub> hubContext,
            IUserService userService,
            IChatroomService chatroomService,
            IService<ChatMessage> chatMessageService)
        {
            _hubContext = hubContext;
            _userService = userService;
            _chatroomService = chatroomService;
            _chatMessageService = chatMessageService;
        }

        [HttpDelete]
        [Route("api/chat/remove")]
        public async Task<ActionResult> Remove(int id)
        {
            await _chatroomService.RemoveChatroom(id);
            return Ok();
        }

        [HttpPost]
        [Route("api/chatroom/create")]
        public async Task<ActionResult<int>> Create()
        {
            var allChatrooms = await _chatroomService.GetAllWithUsers();
            var id = await _chatroomService.AddChatroomAsync(new Chatroom());

            return Ok(id);
        }

        [HttpPost]
        [Route("api/chatroom/join")]
        public async Task<ActionResult> Join(ChatroomJoinDto dto)
        {
            var user = await _userService.GetAsync(dto.UserId);
            var oldChatroomId = user.ChatroomId.ToString();
            var allChatrooms = await _chatroomService.GetAllWithUsers();

            var chatroom = await _chatroomService.AddUserToChatroom(dto.ChatroomId, user);

            if (oldChatroomId != null)
            {
                await _hubContext.Groups.RemoveFromGroupAsync(dto.ConnectionId, oldChatroomId);
                await _hubContext.Clients.Group(oldChatroomId).SendAsync("UserDisconnected", user.UserName);
            }

            await _hubContext.Groups.AddToGroupAsync(dto.ConnectionId, dto.ChatroomId.ToString());
            await _hubContext.Clients.Group(dto.ChatroomId.ToString())
                .SendAsync("UserConnected", user.UserName);
            await _hubContext.Clients.Group(dto.ChatroomId.ToString())
                .SendAsync("UpdateUsersList", chatroom.Users);
            await _hubContext.Clients.All.SendAsync("RoomsUpdated", allChatrooms);

            return Ok();
        }

        [HttpPost]
        [Route("api/chatroom/leave")]
        public async Task<ActionResult> Leave(ChatroomJoinDto dto)
        {
            var user = await _userService.GetAsync(dto.UserId);
            var allChatrooms = await _chatroomService.GetAllWithUsers();
            var chatroom = await _chatroomService.RemoveUserFromChatroom(dto.ChatroomId, user);

            await _hubContext.Groups.RemoveFromGroupAsync(dto.ConnectionId, dto.ChatroomId.ToString());
            await _hubContext.Clients.Group(dto.ChatroomId.ToString())
                .SendAsync("UserDisconnected", user.UserName);
            await _hubContext.Clients.Group(dto.ChatroomId.ToString())
                .SendAsync("UpdateUsersList", chatroom.Users);
            await _hubContext.Clients.All.SendAsync("RoomsUpdated", allChatrooms);

            return Ok();
        }

        [HttpGet]
        [Route("api/chatroom/all")]
        public async Task<ActionResult<List<Chatroom>>> GetAll()
        {
            var allChatrooms = await _chatroomService.GetAllWithUsers();

            return Ok(allChatrooms);
        }

        [HttpPost]
        [Route("api/chatroom/send")]
        public async Task<ActionResult> Send(ChatMessage chatMessage)
        {
            await _chatMessageService.AddAsync(chatMessage);
            return Ok();
        }

        [HttpGet]
        [Route("api/chatroom/history")]
        public async Task<ActionResult<List<ChatMessage>>> GetChatHistory(int chatroomId)
        {
            var chatMessages = await _chatMessageService
                .GetAllAsQueryable(c => c.ChatroomId == chatroomId)
                .ToListAsync();

            return Ok(chatMessages);
        }

        [HttpGet]
        [Route("api/chatroom/users")]
        public async Task<ActionResult<List<User>>> GetConnectedUsers(int chatroomId)
        {
            var chatroom = await _chatroomService.GetChatroomAsync(chatroomId);

            return Ok(chatroom.Users);
        }

    }
}