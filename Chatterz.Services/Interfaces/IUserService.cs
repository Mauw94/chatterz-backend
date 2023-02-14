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
        /// Set the user as logged in.
        /// </summary>
        Task SetLoggedIn(User user);

        /// <summary>
        /// Logout user.
        /// </summary>
        Task<User> Logout(int userId);

        /// <summary>
        /// Update user connection id when (chat) signalR connection is established.
        /// </summary>
        Task UpdateConnectionInfo(int id, string connectionId);

        /// <summary>
        /// Check if a wordguesser game is still in progress.
        /// </summary>
        Task<int> CheckWordGuesserInProgress(int userId);

        /// <summary>
        /// Update user connection id when (game) signalR connection is established.
        /// </summary>
        Task UpdateGameConnectionInfo(int id, string connectionId);

        /// <summary>
        /// Disconnect from current wordguesser game.
        /// </summary>
        Task DisconnectFromWordguesser(int userId);
    }
}