using FiveKenPo.Models;

namespace FiveKenPo.Services
{
    public class GameService
    {
        private readonly Round _currentRound;

        private static readonly Dictionary<MoveType, List<MoveType>> WinMoves = new()
        {
            { MoveType.Rock, new List<MoveType> { MoveType.Scissors, MoveType.Lizard } },
            { MoveType.Paper, new List<MoveType> { MoveType.Rock, MoveType.Spock } },
            { MoveType.Scissors, new List<MoveType> { MoveType.Paper, MoveType.Lizard } },
            { MoveType.Spock, new List<MoveType> { MoveType.Scissors, MoveType.Rock } },
            { MoveType.Lizard, new List<MoveType> { MoveType.Spock, MoveType.Paper } }
        };

        public GameService()
        {
            _currentRound = new Round();
        }

        public IEnumerable<Player> GetPlayers()
        {
            return _currentRound.GetPlayers();
        }

        public Player GetPlayer(Guid playerId)
        {
            return _currentRound.GetPlayer(playerId);
        }

        public void AddPlayer(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Nome do jogador não pode ser vazio.");
            }
            var player = new Player(name);
            _currentRound.AddPlayer(player);
        }

        public void RemovePlayer(Guid playerId)
        {
            _currentRound.RemovePlayer(playerId);
        }

        public void AddMove(Guid playerId, MoveType move)
        {
            if (!Enum.IsDefined(typeof(MoveType), move))
            {
                throw new ArgumentException("Movimento inválido.");
            }
            _currentRound.AddMove(playerId, move);
        }

        public Player? FinishRound()
        {
            var players = _currentRound.GetPlayers().ToList();
            if (players.Count < 2)
            {
                throw new InvalidOperationException("É necessário pelo menos dois jogadores para finalizar a rodada.");
            }

            // O vencedor é aquele que derrotou maior número de oponentes.
            Player? winner = null;
            int bestWins = 0;
            foreach (var player in players)
            {
                if (player.Move == null)
                {
                    throw new InvalidOperationException($"Jogador {player.Name} ainda não jogou.");
                }

                int currentWins = 0;
                foreach (var opponent in players)
                {
                    if (opponent.Move == null)
                    {
                        throw new InvalidOperationException($"Jogador {opponent.Name} ainda não jogou.");
                    }

                    if (player.Id == opponent.Id) continue;
                    if (WinMoves[player.Move.Value].Contains(opponent.Move.Value))
                    {
                        currentWins++;
                    }
                }

                // Se houver empate, não tem vencedor
                if (currentWins == bestWins)
                {
                    winner = null;
                }
                else if (currentWins > bestWins)
                {
                    bestWins = currentWins;
                    winner = player;
                }
            }
            _currentRound.ResetRound();

            return winner;
        }
    }
}
