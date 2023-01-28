using Chatterz.Domain.Models;

namespace Chatterz.Services.Interfaces
{
    public interface IUserService : IService<User>
    {
        /// <summary>
        /// Login the user.
        /// </summary>
        Task<User> Login(string userName, string password);

        /// <summary>
        /// Logout user.
        /// </summary>
        Task<User> Logout(int userId);

        /// <summary>
        /// Update user connection id when signalR connection is established.
        /// </summary>
        Task UpdateConnectionInfo(int id, string connectionId);

        /// <summary>
        /// Check if a wordguesser game is still in progress.
        /// </summary>
        Task<bool> CheckWordGuesserInProgress(int userId);
    }
}