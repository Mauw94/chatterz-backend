using Chatterz.Domain.DTO;
using Chatterz.HUBS.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Chatterz.HUBS
{
    public class GameHub : Hub<IGameHub>
    {
        public async Task SendAsync(WordGuesserSendDto dto)
        {
            await Clients.Group(dto.GameroomId).GameUpdateReceievedFromHub(dto.GuessedWord);
        }
    }
}