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
            var gameId = await _gameService.Create();
            return Ok(gameId);
        }

        [HttpPost]
        [Route("api/game/wordguesser/guess")]
        public async Task<ActionResult> Guess(int gameId)
        {
            await _gameService.Guess(gameId);
            return Ok();
        }

        [HttpPost]
        [Route("api/game/wordguesser/connect")]
        public async Task<ActionResult> Connect(GameConnectDto dto)
        {
            var game = await _gameService.GetIncludingPlayers(dto.GameId);

            if (game.Players.Count >= game.MaxPlayers)
                return BadRequest("Game is full, you cannot join this game anymore.");

            await _gameService.AddPlayer(game, dto.Player);

            await _gameManager.AddPlayerToGameGroup("wordguesser" + game.Id, dto.ConnectionId);
            await _gameManager.SendGameroomUpdate("wordguesser" + game.Id, dto.Player.UserName + " connected");

            await _userService.UpdateGameConnectionInfo(dto.Player.Id, dto.ConnectionId);

            return Ok();
        }

        [HttpPost]
        [Route("api/game/wordguesser/disconnect")]
        public async Task<ActionResult> Disconnect(int gameId, string connectionId, int playerId)
        {
            var game = await _gameService.GetIncludingPlayers(gameId);
            var player = game.Players.Where(p => p.Id == playerId).First();

            await _gameManager.RemovePlayerFromGameGroup("wordguesser" + game.Id, connectionId);
            await _gameManager.GameEnded("wordguesser" + game.Id, player.UserName + " has left, the game has ended, you win!");
            await _userService.DisconnectFromWordguesser(playerId);

            var opponent = game.Players.FirstOrDefault(p => p.Id != playerId);

            if (!game.IsGameOver) // player who disconnects after the first player 
                                  // has disconnected should not trigger another game end event
                await _gameService.EndGame(game.Id, opponent == null ? null : opponent.Id);

            return Ok();
        }

        [HttpGet]
        [Route("api/game/wordguesser/win")]
        public async Task<ActionResult> GameWin(int gameId, int playerId)
        {
            var game = await _gameService.GetIncludingPlayers(gameId);
            var player = game.Players.Where(p => p.Id == playerId).First();
            var otherPlayer = game.Players.Where(p => p.Id != playerId).First();

            await _gameManager.GameWin("wordguesser" + gameId, "The winning word was: " + game.WordToGuess
                + "\n" + player.UserName + " has won!");
            await _gameService.EndGame(gameId, playerId);

            // dc both players from the game
            await _userService.DisconnectFromWordguesser(player.Id);
            await _userService.DisconnectFromWordguesser(otherPlayer.Id);

            return Ok();
        }

        [HttpGet]
        [Route("api/game/wordguesser/start")]
        public async Task<ActionResult> Start(int gameId, int wordLength)
        {
            var game = await _gameService.GetIncludingPlayers(gameId);
            await _gameService.Start(game);
            await _gameManager.SendGameroomUpdate("wordguesser" + game.Id, "game started, let's GOO!");
            await _gameService.GenerateRandomWord(game, wordLength);

            var wordGuesserDto = new WordGuesserDto()
            {
                GameroomId = "wordguesser" + game.Id,
                PlayerIds = game.Players.Select(x => x.Id).ToList(),
                GuessedWord = "",
                PlayerToPlay = DecidePlayerTurn(game.Players),
                WordToGuess = game.WordToGuess
            };

            await _gameManager.StartGame("wordguesser" + game.Id, wordGuesserDto);

            return Ok();
        }

        [HttpGet]
        [Route("api/game/wordguesser/can_start")]
        public async Task<ActionResult> CanStartGame(int gameId)
        {
            var game = await _gameService.GetIncludingPlayers(gameId);

            if (game.Players.Count <= 2)
                await _gameManager.CanStartGame("wordguesser" + game.Id, false);

            if (game.Players.Count == 2)
                await _gameManager.CanStartGame("wordguesser" + game.Id, true);

            return Ok();
        }

        private int DecidePlayerTurn(List<User> users)
        {
            var rnd = new Random().Next(users.Count);
            return users[rnd].Id;
        }
    }
}