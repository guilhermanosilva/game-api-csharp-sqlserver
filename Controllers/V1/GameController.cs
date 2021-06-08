using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using game.api.Models.InputModel;
using game.api.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace game.api.Controllers
{
    [Route("api/V1/[controller]")]
    [ApiController]

    public class GameController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<GameViewModel>>> GetGame()
        {
            return Ok();
        }

        [HttpGet("{idGame:guid}")]
        public async Task<ActionResult<GameViewModel>> GetGame(Guid idGame)
        {
            return Ok();
        }


        [HttpPost]
        public async Task<ActionResult<GameViewModel>> PostGame(GameInputModel game)
        {
            return Ok();
        }

        [HttpPut("idJogo:guid")]
        public async Task<ActionResult> UpdateGame(Guid idJogo, GameInputModel game)
        {
            return Ok();
        }

        [HttpDelete("idJogo:guid")]
        public async Task<ActionResult> DeleteGame(Guid idJogo)
        {
            return Ok();
        }
    }
}