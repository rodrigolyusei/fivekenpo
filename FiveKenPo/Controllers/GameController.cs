using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FiveKenPo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        // POST: api/<GameController>/player
        [HttpPost("player")]
        public IActionResult AddPlayer([FromBody] string playerName)
        {
            return Ok($"Jogador {playerName} cadastrado com sucesso.");
        }

        // DELETE: api/<GameController>/player
        [HttpDelete("player/{id}")]
        public IActionResult RemovePlayer(int id)
        {
            return Ok($"Jogador com ID {id} removido com sucesso.");
        }

        // POST: api/<GameController>/move
        [HttpPost("move")]
        public IActionResult AddMove([FromBody] string move)
        {
            return Ok($"Jogada {move} cadastrado com sucesso.");
        }

        // GET: api/<GameController>/round
        [HttpGet("round")]
        public IActionResult GetRound()
        {
            return Ok();
        }

        // POST: api/<GameController>/finish
        [HttpPost("finish")]
        public IActionResult FinishGame()
        {
            return Ok("Jogo finalizado com sucesso.");
        }
    }
}
