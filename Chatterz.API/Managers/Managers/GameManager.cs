using Chatterz.API.Manages.Interfaces;
using Chatterz.Domain.DTO;
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
            await _hubContext.Groups.AddToGroupAsync(connectionId, groupId);
        }

        public async Task RemovePlayerFromGameGroup(string groupId, string connectionId)
        {
            await _hubContext.Groups.RemoveFromGroupAsync(connectionId, groupId);
        }

        public async Task SendGameroomUpdate(string groupId, string message)
        {
            await _hubContext.Clients.Group(groupId).SendAsync("PlayerJoined", message);
        }

        public async Task CanStartGame(string groupId, bool canStart)
        {
            await _hubContext.Clients.Group(groupId).SendAsync("CanStartGame", canStart);
        }

        public async Task StartGame(string groupId, WordGuesserDto dto)
        {
            await _hubContext.Clients.Group(groupId).SendAsync("StartGame", dto);
        }
    }
}