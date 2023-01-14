using Chatterz.Domain;

namespace Chatterz.API.InMemoryDb
{
    public class ChatroomDb : IChatroomDb
    {
        private readonly Dictionary<string, List<string>> _tempDb = new(); // key: chatroom | value: connected users
        private readonly Dictionary<string, List<ChatMessage>> _chatHistory = new(); // key: chatroom | value: chats
    
        public virtual bool SaveChatroom(string chatroomId, string user)
        {
            if (_tempDb.ContainsKey(chatroomId))
                _tempDb[chatroomId].Add(user);
            else
                if (!_tempDb.TryAdd(chatroomId, new List<string> { user }))
                    return false;

            return true;
        }

        public virtual bool SaveChat(string chatroomId, ChatMessage chatMessage)
        {
            if (_chatHistory.ContainsKey(chatroomId))
                _chatHistory[chatroomId].Add(chatMessage);
            else
                if (!_chatHistory.TryAdd(chatroomId, new List<ChatMessage> { chatMessage }))
                    return false;
            
            return true;
        }

        public virtual List<ChatMessage> GetChatHistory(string chatroomId)
        {
            if (_chatHistory.TryGetValue(chatroomId, out var chatMessages))
                return chatMessages;
            
            throw new KeyNotFoundException($"Could not find chatroom {chatroomId}");
        }

        public virtual List<string> ConnectedUsers(string chatroomId)
        {
            if (_tempDb.TryGetValue(chatroomId, out var connectedUsers))
                return connectedUsers;
            else
                throw new ArgumentException($"Key was not found {chatroomId}");
        }

        public virtual List<string> GetAllChatrooms()
        {
            if (_tempDb.Keys.Any())
                return _tempDb.Keys.ToList();
            
            return null;
        }

    }
}