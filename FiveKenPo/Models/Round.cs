namespace FiveKenPo.Models
{
    public class Round
    {
        public Dictionary<Guid, Player> Players { get; set; }

        public Round()
        {
            Players = new Dictionary<Guid, Player>();
        }

        public void AddPlayer(Player player)
        {
            if (Players.ContainsKey(player.Id))
            {
                throw new ArgumentException("Jogador já existe.");
            }
            Players[player.Id] = player;
        }

        public void RemovePlayer(Guid playerId)
        {
            if (!Players.ContainsKey(playerId))
            {
                throw new KeyNotFoundException("Jogador não cadastrado.");
            }
            Players.Remove(playerId);
        }

        public void AddMove(Guid playerId, MoveType move)
        {
            if (!Players.ContainsKey(playerId))
            {
                throw new KeyNotFoundException("Jogador não cadastrado.");
            }
            Players[playerId].Move = move;
        }

        public IEnumerable<Player> GetPlayers()
        {
            return Players.Values;
        }

        public void FinishRound()
        {
            Players.Clear();
        }
    }
}
