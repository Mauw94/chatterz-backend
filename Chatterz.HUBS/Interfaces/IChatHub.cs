using Chatterz.Domain.Models;

namespace Chatterz.HUBS.Interfaces
{
    public interface IChatHub
    {
        Task MessageReceivedFromHub(ChatMessage message);
    }
}
