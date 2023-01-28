using Chatterz.DataAccess.Interfaces;
using Chatterz.Domain.Models;
using Chatterz.Services.Interfaces;

namespace Chatterz.Services.Services
{
    public class UserService : Service<User>, IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IWordGuesserRepository _wordGuesserRepository;
        public UserService(
            IRepository<User> repository,
            IUserRepository userRepo,
            IWordGuesserRepository wordGuesserRepository)
            : base(repository)
        {
            _userRepo = userRepo;
            _wordGuesserRepository = wordGuesserRepository;
        }

        public async Task<User> Login(string userName, string password)
        {
            return await _userRepo.Login(userName, password);
        }

        public async Task UpdateConnectionInfo(int id, string connectionId)
        {
            await _userRepo.UpdateConnectionInfo(id, connectionId);
        }

        public async Task<User> Logout(int userId)
        {
            return await _userRepo.Logout(userId);
        }

        public async Task<bool> CheckWordGuesserInProgress(int userId)
        {
            var user = await _userRepo.GetAsync(userId);
            if (!user.WordGuesserId.HasValue) return false;

            var game = await _wordGuesserRepository.GetAsync(user.WordGuesserId.Value);
            if (game.IsGameOver) return false;

            return true;
        }
    }
}