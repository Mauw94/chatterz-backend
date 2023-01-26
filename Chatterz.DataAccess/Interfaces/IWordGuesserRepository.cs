using Chatterz.Domain.Models;

namespace Chatterz.DataAccess.Interfaces
{
    public interface IWordGuesserRepository : IRepository<WordGuesser>
    {
        /// <summary>
        /// Start the game.
        /// </summary>
        Task Start(WordGuesser game);
    }
}