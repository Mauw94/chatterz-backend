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

        public async Task<int> Create()
        {
            var game = new WordGuesser();
            ApplicationDbContext.WordGuessers.Add(game);
            await ApplicationDbContext.SaveChangesAsync();

            return game.Id;
        }

        public async Task Start(WordGuesser game)
        {
            game.IsGameStarted = true;
            ApplicationDbContext.WordGuessers.Update(game);
            await ApplicationDbContext.SaveChangesAsync();
        }

        public async Task AddPlayer(WordGuesser game, User player)
        {
            game.Players.Add(player);
            ApplicationDbContext.WordGuessers.Update(game);
            await ApplicationDbContext.SaveChangesAsync();
        }
    }
}