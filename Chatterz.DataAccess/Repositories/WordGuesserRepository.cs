using Chatterz.DataAccess.Interfaces;
using Chatterz.Domain.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<WordGuesser> GetIncludingPlayers(int gameId)
        {
            var game = await ApplicationDbContext.WordGuessers
                .Include(x => x.Players)
                .FirstOrDefaultAsync(x => x.Id == gameId);

            if (game == null) throw new ArgumentException("Could not find wordguesser game.");

            return game;
        }

        public async Task EndGame(int gameId, int winnerId)
        {
            var game = await ApplicationDbContext.WordGuessers
                .FirstOrDefaultAsync(w => w.Id == gameId);

            if (game == null) throw new ArgumentException("Could not find game.");

            if (game.IsGameOver) return;
            
            game.WinnerId = winnerId;
            game.IsGameOver = true;
            game.IsGameStarted = false;

            ApplicationDbContext.WordGuessers.Update(game);
            await ApplicationDbContext.SaveChangesAsync();
        }
    }
}