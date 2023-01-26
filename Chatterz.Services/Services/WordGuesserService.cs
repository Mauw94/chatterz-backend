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

    }
}