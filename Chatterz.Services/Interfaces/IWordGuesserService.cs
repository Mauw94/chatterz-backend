using Chatterz.Domain.Models;

namespace Chatterz.Services.Interfaces
{
    public interface IWordGuesserService : IService<WordGuesser>
    {
        /// <summary>
        /// Generates a random word.
        /// </summary>
        string GenerateRandomWord(int wordLength);

        /// <summary>
        /// Determine which player goes first.
        /// </summary>
        int DetermineFirstTurn(IEnumerable<int> userIds);

        /// <summary>
        /// Create a new game. 
        /// </summary>
        Task<int> Create();

        /// <summary>
        /// Start the game.
        /// </summary>
        Task Start(WordGuesser game);

        /// <summary>
        /// Add a new player to the game. 
        /// </summary>
        Task AddPlayer(WordGuesser game, User player);
    }
}