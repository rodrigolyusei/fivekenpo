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

        // POST: api/<GameController>/player
        [HttpPost("player")]
        public IActionResult AddPlayer([FromBody] string playerName)
        {
            try
            {
                _gameService.AddPlayer(playerName);
                return Ok($"Jogador \"{playerName}\" cadastrado com sucesso.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/<GameController>/player
        [HttpDelete("player/{name}")]
        public IActionResult RemovePlayer(string name)
        {
            try
            {
                Player removedPlayer = _gameService.GetPlayer(name);
                _gameService.RemovePlayer(name);
                return Ok($"Jogador \"{removedPlayer.Name}\" removido com sucesso.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/<GameController>/move
        [HttpPost("move")]
        public IActionResult AddMove([FromBody] string nameMove)
        {
            try
            {
                string name = nameMove.Split(' ')[0];
                string move = nameMove.Split(' ')[1];
                _gameService.AddMove(name, move);
                return Ok($"Jogador \"{name}\" adicionou a jogada {move}.\nUma vez adicionada, não pode ser mais alterada.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/<GameController>/round
        [HttpGet("round")]
        public IActionResult GetRound()
        {
            var players = _gameService.GetPlayers();
            if(players.Count() == 0)
            {
                return Ok("Nenhum jogador cadastrado.");
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
            return Ok(roundStatus.TrimEnd(',', ' ') + ".");
        }

        // POST: api/<GameController>/finish
        [HttpPost("finish")]
        public IActionResult FinishGame()
        {
            try
            {
                Player? winner = _gameService.FinishRound();
                if (winner != null)
                {
                    return Ok($"Rodada finalizada com vencedor: \"{winner.Name}\" Clap! Clap! Clap!");
                }
                else
                {
                    return Ok("Rodada finalizada com empate.");
                }
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
