using Chatterz.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chatterz.API.Controllers
{
    [ApiController]
    public class SpaceInvadersController : ControllerBase
    {
        private readonly ISpaceInvadersService _spaceInvadersService;

        // TODO: have to test all of this and add to context/db
        public SpaceInvadersController(ISpaceInvadersService spaceInvadersService)
        {
            _spaceInvadersService = spaceInvadersService;
        }

        [HttpGet]
        [Route("api/game/spaceinvaders/start")]
        public async Task<ActionResult<int>> Start(int userId)
        {
            var id = await _spaceInvadersService.Start(userId);
            return Ok(id);
        }

        [HttpGet]
        [Route("api/game/spaceinvaders/highscore")]
        public async Task<ActionResult<int>> GetHighScore()
        {
            var highScore = await _spaceInvadersService.GetHighScore();

            return Ok(highScore);
        }

        [HttpPost]
        [Route("api/game/spaceinvaders/savescore")]
        public async Task<ActionResult> SaveScore(int gameId, int score)
        {
            await _spaceInvadersService.SaveScore(gameId, score);
            return Ok();
        }
    }
}