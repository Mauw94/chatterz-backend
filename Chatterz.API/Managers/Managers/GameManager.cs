using Chatterz.API.Manages.Interfaces;
using Chatterz.Domain.Models;
using Chatterz.HUBS;
using Microsoft.AspNetCore.SignalR;

namespace Chatterz.API.Manages.Managers
{
    public class GameManager : IGameManager
    {
        private readonly IHubContext<GameHub> _hubContext;

        public GameManager(IHubContext<GameHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task AddPlayerToGameGroup(string groupId, string connectionId)
        {
            await _hubContext.Groups.AddToGroupAsync(groupId, connectionId);
        }

        public async Task SendPlayerJoinedGroupUpdate(string groupId, User user)
        {
            await _hubContext.Clients.Group(groupId).SendAsync("PlayerJoined", user.UserName);
        }

        public async Task SendStartGame(string groupId, string connectionId)
        {

        }
    }
}