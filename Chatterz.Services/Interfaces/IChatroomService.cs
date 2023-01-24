using Chatterz.Domain.Models;

namespace Chatterz.Services.Interfaces
{
    public interface IChatroomService : IService<Chatroom>
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