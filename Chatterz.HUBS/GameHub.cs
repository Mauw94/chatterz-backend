using Chatterz.Domain.DTO;
using Chatterz.HUBS.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Chatterz.HUBS
{
    public class GameHub : Hub<IGameHub>
    {
        public async Task SendAsync(WordGuesserDto dto)
        {
            dto.PlayerToPlay = SwitchPlayerTurns(dto);
            await Clients.Group(dto.GameroomId).GameUpdateReceievedFromHub(dto);
        }

        private int SwitchPlayerTurns(WordGuesserDto dto) =>
             dto.PlayerToPlay == dto.PlayerIds[0] ? dto.PlayerIds[1] : dto.PlayerIds[0];

    }
}