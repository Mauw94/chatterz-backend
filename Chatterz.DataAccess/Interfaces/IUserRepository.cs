using Chatterz.Domain.Models;

namespace Chatterz.DataAccess.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> Login(string username, string password);
    }
}