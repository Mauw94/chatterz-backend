using Chatterz.Domain.Models;

namespace Chatterz.DataAccess.Interfaces
{
    public interface IWordGuesserRepository : IRepository<WordGuesser>
    {
        /// <summary>
        /// Create a new game. 
        /// </summary>
        Task<int> Create();

        /// <summary>
        /// Add a new player to the game. 
        /// </summary>
        Task AddPlayer(WordGuesser game, User player);

        /// <summary>
        /// Start the game.
        /// </summary>
        Task Start(WordGuesser game);

        /// <summary>
        /// Get the game including the players.
        /// </summary>
        Task<WordGuesser> GetIncludingPlayers(int gameId);

        /// <summary>
        /// Game ends en we set the winner.
        /// </summary>
        Task EndGame(int gameId, int? winnerId);
    }
}