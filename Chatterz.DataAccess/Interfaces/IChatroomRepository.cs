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
    }
}