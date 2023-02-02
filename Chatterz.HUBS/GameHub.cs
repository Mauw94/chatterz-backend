using Chatterz.Domain.DTO;
using Chatterz.Domain.Models;
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

        public async Task BroadcastAsync(ChatMessage message)
        {
            await Clients.Group("wordguesser" + message.ChatroomId.ToString()).MessageReceivedFromHub(message);
        }

        private int SwitchPlayerTurns(WordGuesserDto dto) =>
             dto.PlayerToPlay == dto.PlayerIds[0] ? dto.PlayerIds[1] : dto.PlayerIds[0];

    }
}