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

        /// <summary>
        /// Cadastra um novo jogador na rodada.
        /// </summary>
        /// <param name="request">Nome do jogador não nulo a ser cadastrado.</param>
        /// <returns>Resultado do cadastro.</returns>
        [HttpPost("player")]
        public IActionResult AddPlayer([FromBody] AddPlayerRequest request)
        {
            try
            {
                _gameService.AddPlayer(request.Name);
                return Ok(new { message = $"Jogador '{request.Name}' cadastrado com sucesso." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Remove o jogador indicado pelo nome da rodada.
        /// </summary>
        /// <param name="name">Nome do jogador a ser removido.</param>
        /// <returns>Resultado da remoção</returns>
        [HttpDelete("player/{name}")]
        public IActionResult RemovePlayer(string name)
        {
            try
            {
                Player removedPlayer = _gameService.GetPlayer(name);
                _gameService.RemovePlayer(name);
                return Ok(new { message = $"Jogador '{removedPlayer.Name}' removido com sucesso." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Adiciona uma jogada para um jogador na rodada.
        /// </summary>
        /// <param name="request">Nome do jogador e a sua jogada.</param>
        /// <returns>Resultado da adição.</returns>
        [HttpPost("move")]
        public IActionResult AddMove([FromBody] AddMoveRequest request)
        {
            try
            {
                _gameService.AddMove(request.Name, request.Move);
                return Ok(new { message = $"'{request.Name}' jogou '{request.Move}' e não pode ser mais alterado." });
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

        /// <summary>
        /// Retorna o status da rodada, indicando quais jogadores já jogaram e quais ainda precisam jogar.
        /// </summary>
        /// <returns>Todos os jogadores com respectiva indicação se já jogou ou não.
        /// Caso não haja nenhum jogador retorna a mensagem sobre.</returns>
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
                    roundStatus += $"'{player.Name}' precisa jogar,";
                }
                else
                {
                    roundStatus += $"'{player.Name}' já jogou,";
                }
                roundStatus += " ";
            }
            return Ok(new { message = roundStatus.TrimEnd(',', ' ') + "." });
        }

        /// <summary>
        /// Finaliza a rodada e determina o vencedor.
        /// </summary>
        /// <returns>Retorna o vencedor que pode ser nulo. E uma mensagem de finalização.</returns>
        [HttpPost("finish")]
        public IActionResult FinishGame()
        {
            try
            {
                Player? winner = _gameService.FinishRound();
                if (winner != null)
                {
                    return Ok(new { winner = winner.Name, message = $"Parabéns '{winner.Name}'! Clap Clap Clap" });
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
