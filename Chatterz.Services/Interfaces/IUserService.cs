using Chatterz.Domain.Models;

namespace Chatterz.Services.Interfaces
{
    public interface IUserService : IService<User>
    {
        Task<User> Login(string userName, string password);
    }
}