using Chatterz.DataAccess.Interfaces;
using Chatterz.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Chatterz.DataAccess.Repositories
{
    public class SpaceInvadersRepository : Repository<SpaceInvaders>, ISpaceInvadersRepository
    {
        public SpaceInvadersRepository(ApplicationDbContext context)
            : base(context)
        { }

        public async Task<int> GetHighScore()
        {
            return await ApplicationDbContext.SpaceInvaders
                .Select(x => x.Score)
                .MaxAsync();
        }
    }
}