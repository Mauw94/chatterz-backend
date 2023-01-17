using Chatterz.Domain;

namespace Chatterz.API.InMemoryDb
{
    public interface IChatroomDb
    {
        string? Join(string chatroomId, string userId);
        bool SaveChat(string chatroomId, ChatMessage chatMessage);
        List<ChatMessage> GetChatHistory(string chatroomId);
        List<string> ConnectedUsers(string chatroomId);
        Dictionary<string, List<string>> GetAllChatrooms();
    }
}