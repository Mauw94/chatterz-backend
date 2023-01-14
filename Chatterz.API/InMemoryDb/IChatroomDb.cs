using Chatterz.Domain;

namespace Chatterz.API.InMemoryDb
{
    public interface IChatroomDb
    {
        bool SaveChatroom(string chatroomId, string user);
        bool SaveChat(string chatroomId, ChatMessage chatMessage);
        List<ChatMessage> GetChatHistory(string chatroomId);
        List<string> ConnectedUsers(string chatroomId);
        List<string> GetAllChatrooms();
    }
}