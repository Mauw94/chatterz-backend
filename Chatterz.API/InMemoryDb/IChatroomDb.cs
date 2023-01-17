using Chatterz.Domain;

namespace Chatterz.API.InMemoryDb
{
    public interface IChatroomDb
    {
        string? Join(string chatroomId, string userId);
        bool SaveChat(ChatMessage chatMessage);
        bool SaveChatroom(string chatroomId);
        List<ChatMessage> GetChatHistory(string chatroomId);
        List<string> ConnectedUsers(string chatroomId);
        Dictionary<string, List<string>> GetAllChatrooms();
    }
}