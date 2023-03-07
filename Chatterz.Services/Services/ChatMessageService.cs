using Chatterz.DataAccess.Interfaces;
using Chatterz.Domain.Models;
using Chatterz.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chatterz.Services.Services;

public class ChatMessageService : Service<ChatMessage>, IChatMessageService
{
    private readonly IChatMessageRepository _chatMessageRepository;

    public ChatMessageService(IChatMessageRepository chatMessageRepository, IRepository<ChatMessage> repository) : base(repository)
    {
        _chatMessageRepository = chatMessageRepository;
    }

    public async Task<List<ChatMessage>> GetChatHistory(int chatroomId)
    {
        return await _chatMessageRepository
            .GetAllAsQueryable(c => c.ChatroomId == chatroomId)
            .Where(c => c.DateTime.Date.Day == DateTime.Now.Date.Day)
            .ToListAsync();
    }
}