using Chatterz.API.Manages.Interfaces;
using Chatterz.Domain.DTO;
using Chatterz.Domain.Models;
using Chatterz.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chatterz.API.Controllers.GameControllers
{
    [ApiController]
    public class WordGuesserController : ControllerBase
    {
        private readonly IWordGuesserService _gameService;
        private readonly IUserService _userService;
        private readonly IGameManager _gameManager;

        // TODO: create gamehub
        // create seperate manager to handle signalR gamehub?
        // add players to gamehub group when connecting
        // 
        // when games full send activation to the connected players
        // game is being played
        // update every turn

        public WordGuesserController(IWordGuesserService service, IUserService userService, IGameManager gameManager)
        {
            _gameService = service;
            _gameManager = gameManager;
            _userService = userService;
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
        public async Task<ActionResult<WordGuesser>> Connect(GameConnectDto dto)
        {
            var game = await _gameService.GetAsync(dto.GameId);
            // TODO: include players in get
            // check if same player tries to reconnect to the game -> allow it
            // if another player tries to connect and game is full -> don't allow

            if (game.Players.Count >= game.MaxPlayers)
                return BadRequest("Game is full, you cannot join this game anymore.");

            await _gameService.AddPlayer(game, dto.Player);

            await _gameManager.AddPlayerToGameGroup("wordguesser" + game.Id, dto.ConnectionId);
            await _gameManager.SendGameroomUpdate("wordguesser" + game.Id, dto.Player.UserName + " connected");

            await _userService.UpdateGameConnectionInfo(dto.Player.Id, dto.ConnectionId);

            return Ok(game);
        }

        [HttpPost]
        [Route("api/game/wordguesser/disconnect")]
        public async Task<ActionResult> Disconnect(int gameId, string connectionId, User player)
        {
            var game = await _gameService.GetAsync(gameId);

            await _gameManager.RemovePlayerFromGameGroup("wordguesser" + game.Id, connectionId);
            await _gameManager.SendGameroomUpdate("wordguesser" + game.Id, player.UserName + " disconnected");

            return Ok();
        }

        [HttpGet]
        [Route("api/game/wordguesser/start")]
        public async Task<ActionResult> Start(int gameId)
        {
            // TODO: determine player turn here
            // pass it in the dto or something
            var game = await _gameService.GetAsync(gameId);
            await _gameService.Start(game);
            await _gameManager.SendGameroomUpdate("wordguesser" + game.Id, "game started, let's GOO!");

            return Ok();
        }

        [HttpGet]
        [Route("api/game/wordguesser/can_start")]
        public async Task<ActionResult<bool>> CanStartGame(int gameId)
        {
            var game = await _gameService.GetIncludingPlayers(gameId);
            if (game.Players.Count < 2)
                return Ok(false);
            
            return Ok(true);
        }

        // TODO: method in gamehub to update state and send guesses over the connection     
        // make gamehub interface
        // invoke method when playing the game
    }
}