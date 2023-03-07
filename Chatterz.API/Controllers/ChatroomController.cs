using Chatterz.Domain.Models;
using Chatterz.Domain.DTO;
using Chatterz.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Chatterz.API.Manages.Interfaces;

namespace Chatterz.API.Controllers
{
    [ApiController]
    public class ChatroomController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IChatroomService _chatroomService;
        private readonly ISignalRManager _signalRManager;
        private readonly IChatMessageService _chatMessageService;

        public ChatroomController(
            IUserService userService,
            IChatroomService chatroomService,
            ISignalRManager signalRManager,
            IChatMessageService chatMessageService)
        {
            _userService = userService;
            _chatroomService = chatroomService;
            _signalRManager = signalRManager;
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
            var id = await _chatroomService.AddChatroomAsync();

            return Ok(id);
        }

        [HttpPost]
        [Route("api/chatroom/join")]
        public async Task<ActionResult> Join(ChatroomJoinDto dto)
        {
            var user = await _userService.GetAsync(dto.UserId);
            await _signalRManager.UpdateChatroomsOnUserJoin(user, dto.ChatroomId, dto.ConnectionId);

            return Ok();
        }

        [HttpPost]
        [Route("api/chatroom/leave")]
        public async Task<ActionResult> Leave(ChatroomJoinDto dto)
        {
            var user = await _userService.GetAsync(dto.UserId);
            await _signalRManager.UpdateChatroomsOnUserLeave(user, dto.ChatroomId, dto.ConnectionId);

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
            var chatMessages = await _chatMessageService.GetChatHistory(chatroomId);

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