using Chatterz.DataAccess.Interfaces;
using Chatterz.Domain.Models;

namespace Chatterz.DataAccess.Repositories
{
    public class ChatroomRepository : Repository<Chatroom>, IChatroomRepository
    {
        public ChatroomRepository() { }

        public ChatroomRepository(ApplicationDbContext context) : base(context) { }

    }
}