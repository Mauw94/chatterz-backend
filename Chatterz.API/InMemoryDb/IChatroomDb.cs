using Chatterz.Domain;
using Chatterz.Domain.DTO;

namespace Chatterz.API.InMemoryDb
{
    public interface IChatroomDb
    {
        string? Join(string chatroomId, string userId);
        void Leave(string chatroomId, string userId);
        bool SaveChat(ChatMessage chatMessage);
        bool SaveChatroom(string chatroomId);
        ChatroomDto Get(string id);
        List<ChatMessage> GetChatHistory(string chatroomId);
        List<string> ConnectedUsers(string chatroomId);
        Dictionary<string, List<string>> GetAllChatrooms();
    }
}