using Chatterz.Domain;

namespace Chatterz.HUBS.Interfaces
{
    public interface IChatHub
    {
        Task MessageReceivedFromHub(ChatMessage message);
        Task NewUserConnection(string message);
    }
}
