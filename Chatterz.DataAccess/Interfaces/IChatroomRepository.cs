using Chatterz.Domain.Models;

namespace Chatterz.DataAccess.Interfaces
{
    public interface IChatroomRepository : IRepository<Chatroom>
    {
        /// <summary>
        /// Add chatroom and return id.
        /// </summary>
        Task<int> AddChatroomAsync(Chatroom chatroom);

        /// <summary>
        /// Get the chatroom and users connected to it.
        /// </summary>
        Task<Chatroom> GetChatroomAsync(int id);

        /// <summary>
        /// Add a user to the chatroom.
        /// </summary>
        Task<Chatroom> AddUserToChatroom(int id, User user);

        /// <summary>
        /// Remove a user from the chatroom.
        /// </summary>
        Task<Chatroom> RemoveUserFromChatroom(int id, User user);

        /// <summary>
        /// Get all chatrooms including users.
        /// </summary>
        Task<List<Chatroom>> GetAllWithUsers();

        /// <summary>
        /// Remove a chatroom.
        /// </summary>
        Task RemoveChatroom(int id);
    }
}