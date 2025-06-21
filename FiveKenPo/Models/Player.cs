namespace FiveKenPo.Models
{
    public enum MoveType
    {
        Rock,
        Paper,
        Scissors,
        Spock,
        Lizard
    }

    public class Player
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public MoveType? Move { get; set; }

        public Player(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Move = null;
        }
    }
}
