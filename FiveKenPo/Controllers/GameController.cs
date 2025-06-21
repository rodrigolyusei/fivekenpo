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
                return Ok($"Jogador {playerName} cadastrado com sucesso.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/<GameController>/round
        [HttpGet("player/{id}")]
        public IActionResult GetPlayer(string id)
        {
            Player player = _gameService.GetPlayer(Guid.Parse(id));

            if (player == null)
            {
                return NotFound($"Jogador com ID {id} não encontrado.");
            }
            return Ok($"O jogador com ID {id} é \"{player.Name}\"");
        }

        // DELETE: api/<GameController>/player
        [HttpDelete("player/{id}")]
        public IActionResult RemovePlayer(string id)
        {
            try
            {
                Guid playerId = Guid.Parse(id);
                Player removedPlayer = _gameService.GetPlayer(playerId);

                _gameService.RemovePlayer(playerId);
                return Ok($"Jogador {removedPlayer.Name} removido com sucesso.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/<GameController>/move
        [HttpPost("move")]
        public IActionResult AddMove([FromBody]string id, string move)
        {
            try
            {
                Guid playerId = Guid.Parse(id);
                MoveType playerMove = Enum.Parse<MoveType>(move, true);

                _gameService.AddMove(playerId, playerMove);
                return Ok($"Jogador {id} adicionou a jogada {move}.");
            }
            catch (ArgumentException ex)
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
                    return Ok($"Rodada finalizada. O vencedor é {winner.Name}.");
                }
                else
                {
                    return Ok("Rodada finalizada. Não houve vencedor.");
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
