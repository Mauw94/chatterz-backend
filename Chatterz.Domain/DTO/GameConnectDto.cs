using Chatterz.Domain.Models;

namespace Chatterz.Domain.DTO
{
    public class GameConnectDto
    {
        public int GameId { get; set; }
        public User Player { get; set; }
        public string ConnectionId { get; set; }
    }
}