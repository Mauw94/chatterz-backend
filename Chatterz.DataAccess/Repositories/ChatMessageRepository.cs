using Chatterz.DataAccess.Interfaces;
using Chatterz.Domain.Models;

namespace Chatterz.DataAccess.Repositories;

public class ChatMessageRepository : Repository<ChatMessage>, IChatMessageRepository
{
    public ChatMessageRepository() { }

    public ChatMessageRepository(ApplicationDbContext context) : base(context) { }
}