using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using game.api.Exceptions;
using game.api.Models.InputModel;
using game.api.Models.ViewModel;
using game.api.Services;
using Microsoft.AspNetCore.Mvc;

namespace game.api.Controllers
{
    [Route("api/V1/[controller]")]
    [ApiController]

    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> GetGame([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int amount = 5)
        {
            var games = await _gameService.GetGame(page, amount);

            if (games.Count() == 0)
                return NoContent();

            return Ok(games);
        }

        [HttpGet("{idGame:guid}")]
        public async Task<ActionResult<GameViewModel>> GetGame([FromRoute] Guid idGame)
        {
            var game = await _gameService.GetGame(idGame);

            if (game == null)
                return NoContent();

            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult<GameViewModel>> PostGame([FromBody] GameInputModel game)
        {
            try
            {
                var gameResult = await _gameService.InsertGame(game);
                return Ok(gameResult);
            }
            catch (GameAlreadyRegistered)
            {
                return UnprocessableEntity("There is already a game with this name for this producer");
            }
        }

        [HttpPut("idGame:guid")]
        public async Task<ActionResult> UpdateGame(Guid idGame, GameInputModel game)
        {
            try
            {
                await _gameService.UpdateGame(idGame, game);
                return Ok();
            }
            catch (GameNotRegistered)
            {
                return NotFound("Game not found to update");
            }
        }

        [HttpDelete("idGame:guid")]
        public async Task<ActionResult> DeleteGame(Guid idGame)
        {
            try
            {
                await _gameService.DeleteGame(idGame);
                return Ok();
            }
            catch (GameNotRegistered)
            {
                return NotFound("Game not found to delete");
            }
        }
    }
}