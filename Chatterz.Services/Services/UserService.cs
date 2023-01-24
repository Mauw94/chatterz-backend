using Chatterz.DataAccess.Interfaces;
using Chatterz.Domain.Models;
using Chatterz.Services.Interfaces;

namespace Chatterz.Services.Services
{
    public class UserService : Service<User>, IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IRepository<User> repository, IUserRepository userRepo)
            : base(repository)
        {
            _userRepo = userRepo;
        }

        public async Task<User> Login(string userName, string password)
        {
            return await _userRepo.Login(userName, password);
        }

        public async Task UpdateConnectionInfo(int id, string connectionId)
        {
            await _userRepo.UpdateConnectionInfo(id, connectionId);
        }
    }
}