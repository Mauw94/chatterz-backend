using Chatterz.DataAccess.Interfaces;
using Chatterz.Domain.Models;
using Chatterz.Services.Interfaces;

namespace Chatterz.Services.Services
{
    public class SpaceInvadersService : Service<SpaceInvaders>, ISpaceInvadersService
    {
        private readonly ISpaceInvadersRepository _spaceInvadersRepo;

        public SpaceInvadersService(ISpaceInvadersRepository spaceInvadersRepo, IRepository<SpaceInvaders> repository) 
            : base(repository)
        {
            _spaceInvadersRepo = spaceInvadersRepo;
        }
        
        public async Task<int> Start(int userId)
        {
            var game = new SpaceInvaders();
            game.Player = userId;
            game.IsGameStarted = true;
            await _spaceInvadersRepo.AddAsync(game);
            
            return game.Id;
        }

        public async Task<int> GetHighScore()
        {
            return await _spaceInvadersRepo.GetHighScore();
        }

        public async Task SaveScore(int gameId, int score)
        {
            var game = await _spaceInvadersRepo.GetAsync(gameId);
            game.Score = score;
            game.IsGameOver = true;
            await _spaceInvadersRepo.UpdateAsync(game);
        }
    }
}