using Chatterz.DataAccess.Interfaces;
using Chatterz.Domain.Models;

namespace Chatterz.DataAccess.Repositories
{
    public class WordGuesserRepository : Repository<WordGuesser>, IWordGuesserRepository
    {
        public WordGuesserRepository() { }
        public WordGuesserRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        { }
    }
}