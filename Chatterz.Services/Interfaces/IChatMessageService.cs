using Chatterz.Domain.Models;

namespace Chatterz.Services.Interfaces;

public interface IChatMessageService : IService<ChatMessage>
{
    Task<List<ChatMessage>> GetChatHistory(int chatroomId);
}