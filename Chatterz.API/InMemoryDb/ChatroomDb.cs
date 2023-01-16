using Chatterz.Domain;

namespace Chatterz.API.InMemoryDb
{
    public class ChatroomDb : IChatroomDb
    {
        private readonly Dictionary<string, List<string>> _tempDb = new(); // key: chatroom | value: connected users (userId)
        private readonly Dictionary<string, List<ChatMessage>> _chatHistory = new(); // key: chatroom | value: chats

        public bool SaveChat(string chatroomId, ChatMessage chatMessage)
        {
            if (_chatHistory.ContainsKey(chatroomId))
                _chatHistory[chatroomId].Add(chatMessage);
            else
                if (!_chatHistory.TryAdd(chatroomId, new List<ChatMessage> { chatMessage }))
                return false;

            return true;
        }

        public List<ChatMessage> GetChatHistory(string chatroomId)
        {
            if (_chatHistory.TryGetValue(chatroomId, out var chatMessages))
                return chatMessages;

            throw new KeyNotFoundException($"Could not find chatroom {chatroomId}");
        }

        public List<string> ConnectedUsers(string chatroomId)
        {
            if (_tempDb.TryGetValue(chatroomId, out var connectedUsers))
                return connectedUsers;
            else
                throw new ArgumentException($"Key was not found {chatroomId}");
        }

        public Dictionary<string, List<string>> GetAllChatrooms()
        {
            if (_tempDb.Keys.Any())
                return _tempDb;

            return null;
        }

        public string Join(string chatroomId, string userId)
        {
            var oldChatroomId = _tempDb.Where(x => x.Value.Contains(userId)).FirstOrDefault().Key;

            if (_tempDb.ContainsKey(chatroomId))
                _tempDb[chatroomId].Add(userId);
            else
            {
                if (!_tempDb.TryAdd(chatroomId, new List<string> { userId }))
                    throw new ArgumentException("Can't craete new chatroom or add new user");
            }

            if (oldChatroomId != null)
                _tempDb[oldChatroomId].Remove(userId);

            return oldChatroomId;
        }
    }
}