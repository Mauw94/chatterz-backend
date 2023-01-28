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
        /// Logout user.
        /// </summary>
        Task<User> Logout(int userId);
    }
}