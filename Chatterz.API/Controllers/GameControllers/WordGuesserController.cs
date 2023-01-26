using Chatterz.Domain.Models;
using Chatterz.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chatterz.API.Controllers.GameControllers
{
    [ApiController]
    public class WordGuesserController : ControllerBase
    {
        private readonly IWordGuesserService _gameService;

        public WordGuesserController(IWordGuesserService service)
        {
            _gameService = service;
        }

        [HttpGet]
        [Route("api/game/wordguesser/start")]
        public async Task<ActionResult<WordGuesser>> StartNew(List<User> players)
        {
            var game = new WordGuesser();

            foreach (var player in players)
                game.AddPlayer(player);

            game.WordToGuess = _gameService.GenerateRandomWord(5);
            game.PlayerToStart = _gameService.DetermineFirstTurn(game.Players.Select(p => p.Id));

            await _gameService.Start(game);

            return Ok(game);
        }
    }
}