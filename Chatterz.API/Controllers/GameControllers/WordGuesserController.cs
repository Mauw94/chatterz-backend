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

        [HttpGet]
        [Route("api/game/wordguesser/create")]
        public async Task<ActionResult<int>> Create()
        {
            // TODO: call this when other user accepts the game invite
            // call Connect when launching the game component

            var gameId = await _gameService.Create();
            return Ok(gameId);
        }

        [HttpPost]
        [Route("api/game/wordguesser/connect")]
        public async Task<ActionResult<WordGuesser>> Connect(int gameId, User player, string connectionId)
        {
            var game = await _gameService.GetAsync(gameId);

            if (game.Players.Count >= game.MaxPlayers)
                return BadRequest("Game is full, you cannot join this game anymore.");

            await _gameService.AddPlayer(game, player);

            // TODO save game before this call
            // create specific group id creation method or something, so its always unique
            // with name of the game + its id since thatll always be unique
            // send update to frontend saying that a player connected to this specific gameroom

            await _gameManager.AddPlayerToGameGroup("wordguesser" + game.Id, connectionId);
            await _gameManager.SendGameroomUpdate("wordguesser" + game.Id, player.UserName + " connected");

            return Ok(game);
        }
    
        [HttpGet]
        [Route("api/game/wordguesser/start")]
        public async Task<ActionResult> Start(int gameId)
        {
            var game = await _gameService.GetAsync(gameId);
            await _gameService.Start(game);
            await _gameManager.SendGameroomUpdate("wordguesser" + game.Id, "game started, let's GOO!");

            return Ok();
        }
    }
}