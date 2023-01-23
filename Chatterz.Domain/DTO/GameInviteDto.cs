namespace Chatterz.Domain
{
    public class GameInviteDto
    {
        public User Challenger { get; set; }
        public string UserId { get; set; }
        public string InviteMessage { get; set; }
        public int GameId { get; set; }
    }
}