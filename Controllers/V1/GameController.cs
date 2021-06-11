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

        /// <summary>
        /// Search all games paged
        /// </summary>
        /// <remarks>
        /// It's not possible to return without paging
        /// </remarks>
        /// <param name="page">Page being consulted, Minimum 1</param>
        /// <param name="amount">Number of records per page</param>
        /// <response code="200">Return a list of games</response>
        /// <response code="400">If there are no games</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> GetGame([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int amount = 5)
        {
            var games = await _gameService.GetGame(page, amount);

            if (games.Count() == 0)
                return NoContent();

            return Ok(games);
        }

        /// <summary>
        /// Search game by id
        /// </summary>
        /// <remarks>
        /// It's not possible to return without id
        /// </remarks>
        /// <param name="idGame">Specifc id of a game</param>
        /// <response code="200">Return a specific game</response>
        /// <response code="400">If there are no game fot informed id</response>
        [HttpGet("{idGame:guid}")]
        public async Task<ActionResult<GameViewModel>> GetGame([FromRoute] Guid idGame)
        {
            var game = await _gameService.GetGame(idGame);

            if (game == null)
                return NoContent();

            return Ok(game);
        }

        /// <summary>Insert a new game</summary>
        /// <remarks>Enter all data for a new game</remarks>
        /// <param name="game">JSON object containing the new data</param>
        /// <response code="200">Returns the new game inserted</response>
        /// <response code="400">If it was not possible to insert the new game</response>
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

        /// <summary>Update a game</summary>
        /// <remarks>Enter the ID of the game to be updated and the game data</remarks>
        /// <param name="idGame">Game ID to be updated</param>
        /// <response code="200">successfully updated</response>
        /// <response code="400">Game not found to update</response>
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

        /// <summary>Delete a game</summary>
        /// <remarks>Enter the ID of the game to be deleted</remarks>
        /// <param name="idGame">Game ID to be deleted</param>
        /// <response code="200">successfully deleted</response>
        /// <response code="400">Game not found to delete</response>
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