using Chatterz.Domain.Models;
using Chatterz.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chatterz.API.Controllers.GameControllers
{
    [ApiController]
    public class WordGuesserController : ControllerBase
    {
        private readonly IWordGuesserService _service;

        public WordGuesserController(IWordGuesserService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("api/game/wordguesser/start")]
        public async Task StartNew(List<User> players)
        {
            // TODO: need gamemanager to handle logic 
            // and to initialize a game
            var game = new WordGuesser();
            foreach (var player in players)
                game.AddPlayer(player);
            // game.GenerateRandomWord()
            // game.DecideFirstTurn()
            // game.Start()
        }
    }
}