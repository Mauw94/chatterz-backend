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

        /// <summary>
        /// Add a user to the chatroom.
        /// </summary>
        public async Task<Chatroom> AddUserToChatroom(int id, User user)
        {
            var chatroom = await ApplicationDbContext.Chatrooms
                .Include(c => c.Users)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (chatroom == null)
                throw new ArgumentException($"Could not find chatroom with id: ${id}");

            chatroom.Users.Add(user);
            ApplicationDbContext.Chatrooms.Update(chatroom);
            await ApplicationDbContext.SaveChangesAsync();

            return chatroom;
        }

        public async Task<Chatroom> RemoveUserFromChatroom(int id, User user)
        {
            var chatroom = await ApplicationDbContext.Chatrooms
               .Include(c => c.Users)
               .FirstOrDefaultAsync(c => c.Id == id);

            if (chatroom == null)
                throw new ArgumentException($"Could not find chatroom with id: ${id}");

            chatroom.Users.Remove(user);
            ApplicationDbContext.Chatrooms.Update(chatroom);
            await ApplicationDbContext.SaveChangesAsync();

            return chatroom;
        }

        public async Task<List<Chatroom>> GetAllWithUsers()
        {
            var chatrooms = await ApplicationDbContext.Chatrooms
                .Include(c => c.Users)
                .ToListAsync();

            return chatrooms;
        }
    }
}