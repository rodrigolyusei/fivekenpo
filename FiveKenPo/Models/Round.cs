namespace FiveKenPo.Models
{
    public class Round
    {
        public Dictionary<String, Player> Players { get; set; }

        public Round()
        {
            Players = new Dictionary<String, Player>();
        }

        public void AddPlayer(Player player)
        {
            if (Players.ContainsKey(player.Name))
            {
                throw new ArgumentException("Jogador já cadastrado.");
            }
            Players[player.Name] = player;
        }

        public Player GetPlayer(string playerName)
        {
            if (!Players.TryGetValue(playerName, out Player? player))
            {
                throw new KeyNotFoundException("Jogador não encontrado.");
            }
            return player;
        }

        public IEnumerable<Player> GetPlayers()
        {
            return Players.Values;
        }

        public void RemovePlayer(string playerName)
        {
            if (!Players.ContainsKey(playerName))
            {
                throw new KeyNotFoundException("Jogador não encontrado.");
            }
            Players.Remove(playerName);
        }

        public void AddMove(string playerName, MoveType move)
        {
            if (!Players.TryGetValue(playerName, out Player? player))
            {
                throw new KeyNotFoundException("Jogador não encontrado.");
            }
            if (player.Move != null)
            {
                throw new InvalidOperationException("Jogada já feita e não pode ser alterada.");
            }

            player.Move = move;
        }

        public void ResetRound()
        {
            Players.Clear();
        }
    }
}
