using Chatterz.API.Manages.Interfaces;
using Chatterz.Domain.Models;
using Chatterz.HUBS;
using Chatterz.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Chatterz.API.Manages.Managers
{
    public class SignalRManager : ISignalRManager
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IChatroomService _chatroomService;

        public SignalRManager(IHubContext<ChatHub> hubContext, IChatroomService chatroomService)
        {
            _hubContext = hubContext;
            _chatroomService = chatroomService;
        }

        public async Task UpdateChatroomsOnUserJoin(User user, int chatroomId, string connectionId)
        {
            var oldChatroomId = user.ChatroomId;
            var chatroom = await _chatroomService.AddUserToChatroom(chatroomId, user);
            var allChatrooms = await GetAllChatrooms();

            if (oldChatroomId != null && oldChatroomId != chatroomId)
            {
                await _chatroomService.RemoveUserFromChatroom((int)oldChatroomId, user);
                await _hubContext.Clients.Group(oldChatroomId.Value.ToString()).SendAsync("UserDisconnected", user.UserName);
                await _hubContext.Clients.Group(oldChatroomId.Value.ToString()).SendAsync("UpdateUsersList", chatroom.Users);
            }

            await _hubContext.Groups.AddToGroupAsync(connectionId, chatroomId.ToString());
            await _hubContext.Clients.Group(chatroomId.ToString()).SendAsync("UserConnected", user.UserName);
            await _hubContext.Clients.Group(chatroomId.ToString()).SendAsync("UpdateUsersList", chatroom.Users);
            await _hubContext.Clients.All.SendAsync("RoomsUpdated", allChatrooms);
        }

        public async Task UpdateChatroomsOnUserLeave(User user, int chatroomId, string connectionId)
        {
            var chatroom = await _chatroomService.RemoveUserFromChatroom(chatroomId, user);
            var allChatrooms = await GetAllChatrooms();

            await _hubContext.Clients.Group(chatroomId.ToString()).SendAsync("UserDisconnected", user.UserName);
            await _hubContext.Groups.RemoveFromGroupAsync(connectionId, chatroomId.ToString());
            await _hubContext.Clients.Group(chatroomId.ToString()).SendAsync("UpdateUsersList", chatroom.Users);
            await _hubContext.Clients.All.SendAsync("RoomsUpdated", allChatrooms);
        }

        private async Task<List<Chatroom>> GetAllChatrooms()
        {
            return await _chatroomService.GetAllWithUsers();
        }
    }
}