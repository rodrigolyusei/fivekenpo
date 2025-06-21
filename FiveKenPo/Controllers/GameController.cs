using Microsoft.AspNetCore.Mvc;
using FiveKenPo.Services;
using FiveKenPo.Models;

namespace FiveKenPo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly GameService _gameService;

        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }

        public class AddPlayerRequest
        {
            public required string Name { get; set; }
        }

        public class AddMoveRequest
        {
            public required string Name { get; set; }
            public required string Move { get; set; }
        }

        [HttpPost("player")]
        public IActionResult AddPlayer([FromBody] AddPlayerRequest request)
        {
            try
            {
                _gameService.AddPlayer(request.Name);
                return Ok(new { message = $"\"{request.Name}\" cadastrado com sucesso." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("player/{name}")]
        public IActionResult RemovePlayer(string name)
        {
            try
            {
                Player removedPlayer = _gameService.GetPlayer(name);
                _gameService.RemovePlayer(name);
                return Ok(new { message = $"\"{removedPlayer.Name}\" removido com sucesso." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpPost("move")]
        public IActionResult AddMove([FromBody] AddMoveRequest request)
        {
            try
            {
                _gameService.AddMove(request.Name, request.Move);
                return Ok(new { message = $"\"{request.Name}\" adicionou a jogada {request.Move} e não pode ser mais alterada." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("round")]
        public IActionResult GetRound()
        {
            var players = _gameService.GetPlayers();
            if(!players.Any())
            {
                return Ok(new { message = "Nenhum jogador cadastrado." });
            }

            string roundStatus = "";
            foreach (var player in players)
            {
                if (player.Move == null)
                {
                    roundStatus += $"\"{player.Name}\" precisa jogar,";
                }
                else
                {
                    roundStatus += $"\"{player.Name}\" já jogou,";
                }
                roundStatus += " ";
            }
            return Ok(new { message = roundStatus.TrimEnd(',', ' ') + "." });
        }

        [HttpPost("finish")]
        public IActionResult FinishGame()
        {
            try
            {
                Player? winner = _gameService.FinishRound();
                if (winner != null)
                {
                    return Ok(new { winner = winner.Name, message = $"Parabéns \"{winner.Name}\"! Clap Clap Clap" });
                }
                else
                {
                    return Ok(new { winner = (string?)null, message = "Poxa... Acabou com empate" });
                }
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
