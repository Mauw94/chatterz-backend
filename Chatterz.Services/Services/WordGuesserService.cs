using Chatterz.DataAccess.Interfaces;
using Chatterz.Domain.Models;
using Chatterz.Services.Interfaces;

namespace Chatterz.Services.Services
{
    public class WordGuesserService : Service<WordGuesser>, IWordGuesserService
    {
        private readonly IWordGuesserRepository _wordGuesserRepository;

        public WordGuesserService(IRepository<WordGuesser> repository, IWordGuesserRepository wordGuesserRepository)
            : base(repository)
        {
            _wordGuesserRepository = wordGuesserRepository;
        }

        public async Task<string> GenerateRandomWord(WordGuesser game, int wordLength)
        {
            var path = "C:\\Projects\\fun\\chatterz\\backend\\Chatterz.Services\\word-dictionaries\\";
            var fullPath = path + wordLength + "-letter.txt";
            var words = await File.ReadAllLinesAsync(fullPath);
            var rnd = new Random().Next(0, words.Length);
            var word = words[rnd].ToUpper();

            game.WordToGuess = word;
            await _wordGuesserRepository.UpdateAsync(game);

            return word;
        }

        public async Task<int> Create()
        {
            return await _wordGuesserRepository.Create();
        }

        public async Task Start(WordGuesser game)
        {
            await _wordGuesserRepository.Start(game);
        }

        public async Task AddPlayer(WordGuesser game, User player)
        {
            await _wordGuesserRepository.AddPlayer(game, player);
        }

        public async Task<WordGuesser> GetIncludingPlayers(int gameId)
        {
            return await _wordGuesserRepository.GetIncludingPlayers(gameId);
        }

        public async Task EndGame(int gameId, int? winnerId)
        {
            await _wordGuesserRepository.EndGame(gameId, winnerId);
        }

        public async Task Guess(int gameId)
        {
            await _wordGuesserRepository.Guess(gameId);
        }
    }
}