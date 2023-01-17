using Chatterz.Domain;
using Chatterz.HUBS.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Chatterz.HUBS
{
    public class ChatHub : Hub<IChatHub>
    {
        public async Task BroadcastAsync(ChatMessage message)
        {
            await Clients.Group(message.ChatroomId).MessageReceivedFromHub(message);
        }
    }
}