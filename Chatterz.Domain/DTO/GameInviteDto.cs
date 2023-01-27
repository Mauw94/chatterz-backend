using Chatterz.Domain.Enums;
using Chatterz.Domain.Models;

namespace Chatterz.Domain
{
    public class GameInviteDto
    {
        public User Challenger { get; set; }
        public int UserId { get; set; }
        public string InviteMessage { get; set; }
        public GameType GameType { get; set; }
    }
}