using Chatterz.Domain.Models;

namespace Chatterz.Services.Interfaces
{
    public interface ISpaceInvadersService : IService<SpaceInvaders>
    {
        Task<int> Start(int userId);
        Task<int> GetHighScore();
        Task SaveScore(int gameId, int score);
    }
}