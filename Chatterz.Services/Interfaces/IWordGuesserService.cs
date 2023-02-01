using Chatterz.Domain.Models;

namespace Chatterz.Services.Interfaces
{
    public interface IWordGuesserService : IService<WordGuesser>
    {
        /// <summary>
        /// Generates a random word.
        /// </summary>
        Task<string> GenerateRandomWord(WordGuesser game, int wordLength);

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

        /// <summary>
        /// Get the game including the players.
        /// </summary>
        Task<WordGuesser> GetIncludingPlayers(int gameId);

        /// <summary>
        /// Game ends en we set the winner.
        /// </summary>
        Task EndGame(int gameId, int? winnerId);

        /// <summary>
        /// Increase amount of guesses for a game.
        /// </summary>
        Task Guess(int gameId);
    }
}