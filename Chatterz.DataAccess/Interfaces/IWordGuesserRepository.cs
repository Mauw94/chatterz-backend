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
    }
}