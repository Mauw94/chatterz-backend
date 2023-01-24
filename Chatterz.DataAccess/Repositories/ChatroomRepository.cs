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

            if (!chatroom.Users.Contains(user))
            {
                chatroom.Users.Add(user);
                ApplicationDbContext.Chatrooms.Update(chatroom);
                await ApplicationDbContext.SaveChangesAsync();
            }

            return chatroom;
        }

        public async Task<Chatroom> RemoveUserFromChatroom(int id, User user)
        {
            var chatroom = await ApplicationDbContext.Chatrooms
               .Include(c => c.Users)
               .FirstOrDefaultAsync(c => c.Id == id);

            if (chatroom == null)
                throw new ArgumentException($"Could not find chatroom with id: ${id}");

            if (chatroom.Users.Contains(user))
            {
                chatroom.Users.Remove(user);
                ApplicationDbContext.Chatrooms.Update(chatroom);
                await ApplicationDbContext.SaveChangesAsync();
            }

            return chatroom;
        }

        public async Task<List<Chatroom>> GetAllWithUsers()
        {
            var chatrooms = await ApplicationDbContext.Chatrooms
                .Include(c => c.Users)
                .ToListAsync();

            return chatrooms;
        }

        public async Task RemoveChatroom(int id)
        {
            var chatroomToRemove = await ApplicationDbContext.Chatrooms
                .FirstOrDefaultAsync(c => c.Id == id);

            if (chatroomToRemove == null)
                throw new ArgumentException("Could not find chatroom.");

            var chatMessages = await ApplicationDbContext.ChatMessages
                .Where(c => c.ChatroomId == id)
                .ToListAsync();

            var usersInChatroom = await ApplicationDbContext.Users
                .Where(u => u.ChatroomId == id)
                .ToListAsync();

            foreach (var user in usersInChatroom)
                user.ChatroomId = null;

            ApplicationDbContext.ChatMessages.RemoveRange(chatMessages);
            ApplicationDbContext.Users.UpdateRange(usersInChatroom);
            ApplicationDbContext.Chatrooms.Remove(chatroomToRemove);

            await ApplicationDbContext.SaveChangesAsync();
        }
    }
}