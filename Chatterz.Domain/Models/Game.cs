using Chatterz.Domain.Enums;

namespace Chatterz.Domain.Models
{
    public abstract class Game
    {
        public int Id { get; set; }
        public int MaxPlayers { get; set; } = 2;
        public GameType Type { get; set; }
        public List<User> Players { get; set; }
        public bool IsGameStarted { get; set; } = false;
        public bool IsGameOver { get; set; } = false;
        public int WinnerId { get; set; }
        public int Score { get; set; }

        public Game()
        {
            Players = new();
        }

        public virtual bool AddPlayer(User player)
        {
            if (Players.Count >= MaxPlayers) return false;
            if (!Players.Contains(player))
            {
                Players.Add(player);
                return true;
            }
            return false;
        }
    }
}