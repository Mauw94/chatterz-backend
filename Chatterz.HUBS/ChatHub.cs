using Chatterz.Domain.Models;
using Chatterz.HUBS.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Chatterz.HUBS
{
    public class ChatHub : Hub<IChatHub>
    {
        public async Task BroadcastAsync(ChatMessage message)
        {
            await Clients.Group(message.ChatroomId.ToString()).MessageReceivedFromHub(message);
        }
    }
}