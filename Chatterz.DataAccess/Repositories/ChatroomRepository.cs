using Chatterz.DataAccess.Interfaces;
using Chatterz.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Chatterz.DataAccess.Repositories
{
    public class ChatroomRepository : Repository<Chatroom>, IChatroomRepository
    {
        public ChatroomRepository() { }

        public ChatroomRepository(ApplicationDbContext context) : base(context) { }

        public async Task<int> AddChatroomAsync(Chatroom chatroom)
        {
            await ApplicationDbContext.Chatrooms.AddAsync(chatroom);
            await ApplicationDbContext.SaveChangesAsync();

            return chatroom.Id;
        }

        public async Task<Chatroom> GetChatroomAsync(int id)
        {
            var chatroom = await ApplicationDbContext.Chatrooms
                .Include(c => c.Users)
                .FirstOrDefaultAsync(c => c.Id == id);

            return chatroom;
        }
    }
}