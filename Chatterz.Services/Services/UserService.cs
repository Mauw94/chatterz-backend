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

        public async Task SetLoggedIn(User user)
        {
            user.IsLoggedIn = true;
            await _userRepo.UpdateAsync(user);
        }

        public async Task UpdateConnectionInfo(int id, string connectionId)
        {
            var user = await _userRepo.GetAsync(id);
            user.ConnectionId = connectionId;
            await _userRepo.UpdateAsync(user);
        }

        public async Task<User> Logout(int userId)
        {
            return await _userRepo.Logout(userId);
        }

        public async Task<int> CheckWordGuesserInProgress(int userId)
        {
            var user = await _userRepo.GetAsync(userId);
            if (!user.WordGuesserId.HasValue) return 0;

            var game = await _wordGuesserRepository.GetAsync(user.WordGuesserId.Value);
            if (game.IsGameOver) return 0;

            return game.Id;
        }

        public async Task UpdateGameConnectionInfo(int id, string connectionId)
        {
            var user = await _userRepo.GetAsync(id);
            user.GameConnectionId = connectionId;
            await _userRepo.UpdateAsync(user);
        }

        public async Task DisconnectFromWordguesser(int userId)
        {
            await _userRepo.DisconnectFromWordguesser(userId);
        }
    }
}