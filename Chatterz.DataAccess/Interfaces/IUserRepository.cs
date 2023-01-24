using Chatterz.Domain.Models;

namespace Chatterz.DataAccess.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Login the user.
        /// </summary>
        Task<User> Login(string username, string password);

        /// <summary>
        /// Update user connection id when signalR connection is established.
        /// </summary>
        Task UpdateConnectionInfo(int id, string connectionId);
    }
}