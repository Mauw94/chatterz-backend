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

        public string GenerateRandomWord(int wordLength)
        {
            var word = string.Empty;
            var abc = "abcdefghijklmnopqrstuvwxyz";
            var random = new Random();

            for (int i = 0; i < wordLength; i++)
            {
                var rnd = random.Next(0, abc.Length);
                word += abc[rnd];
            }

            return word;
        }

        public int DetermineFirstTurn(IEnumerable<int> userIds)
        {
            var rnd = new Random().Next(0, userIds.Count() - 1);
            return userIds.ToArray()[rnd];
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
    }
}