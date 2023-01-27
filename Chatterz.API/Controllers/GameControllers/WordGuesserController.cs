using Chatterz.API.Manages.Interfaces;
using Chatterz.Domain.Models;
using Chatterz.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chatterz.API.Controllers.GameControllers
{
    [ApiController]
    public class WordGuesserController : ControllerBase
    {
        private readonly IWordGuesserService _gameService;
        private readonly IGameManager _gameManager;
        // TODO: create gamehub
        // create seperate manager to handle signalR gamehub?
        // add players to gamehub group when connecting
        // 
        // when games full send activation to the connected players
        // game is being played
        // update every turn

        public WordGuesserController(IWordGuesserService service, IGameManager gameManager)
        {
            _gameService = service;
            _gameManager = gameManager;
        }

        [HttpPost]
        [Route("api/game/wordguesser/connect")]
        public async Task<ActionResult<WordGuesser>> Connect(User player, string connectionId)
        {
            var game = new WordGuesser();
            var addedPlayer = game.AddPlayer(player);

            // TODO save game before this call
            // create specific group id creation method or something, so its always unique
            // with name of the game + its id since thatll always be unique
            // send update to frontend saying that a player connected to this specific gameroom
            
            await _gameManager.AddPlayerToGameGroup("TODO_ID", connectionId);

            if (addedPlayer && game.Players.Count == 2)
            {
                game.WordToGuess = _gameService.GenerateRandomWord(5);
                game.PlayerToStart = _gameService.DetermineFirstTurn(game.Players.Select(p => p.Id));
                await _gameService.Start(game);
            }

            return Ok(game);
        }
    }
}