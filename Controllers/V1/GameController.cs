using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace game.api.Controllers
{
    [Route("api/V1/[controller]")]
    [ApiController]

    public class GameController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<object>>> GetGame()
        {
            return Ok();
        }
        
        [HttpGet("{idGame:guid}")]
        public async Task<ActionResult<object>> GetGame(Guid idGame)
        {
            return Ok();
        }


        [HttpPost]
        public async Task<ActionResult<object>> PostGame(object game)
        {
            return Ok();
        }

        [HttpPut("idJogo:guid")]
        public async Task<ActionResult> UpdateGame(Guid idJogo, object game)
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