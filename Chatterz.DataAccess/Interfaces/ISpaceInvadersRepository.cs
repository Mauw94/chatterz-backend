using Chatterz.Domain.Models;

namespace Chatterz.DataAccess.Interfaces
{
    public interface ISpaceInvadersRepository : IRepository<SpaceInvaders>
    {
        Task<int> GetHighScore();
    }
}