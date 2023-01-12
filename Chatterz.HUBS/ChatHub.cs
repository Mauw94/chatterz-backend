using Chatterz.Domain;
using Chatterz.HUBS.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Chatterz.HUBS
{
    public class ChatHub : Hub<IChatHub>
    {
        public async Task BroadcastAsync(ChatMessage message)
        {
            await Clients.All.MessageReceivedFromHub(message);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.NewUserConnection("A new user connected");
        }
    }
}