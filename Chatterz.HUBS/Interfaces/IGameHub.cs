using Chatterz.Domain.DTO;
using Chatterz.Domain.Models;

namespace Chatterz.HUBS.Interfaces
{
    public interface IGameHub
    {
        Task GameUpdateReceievedFromHub(WordGuesserDto wordGuesserDto);

        Task MessageReceivedFromHub(ChatMessage message);
    }
}