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

        public async Task Start(WordGuesser game)
        {
            game.IsGameStarted = true;
            ApplicationDbContext.WordGuessers.Add(game);
            await ApplicationDbContext.SaveChangesAsync();
        }
    }
}