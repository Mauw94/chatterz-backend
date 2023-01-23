namespace Chatterz.Domain
{
    public class GameInviteDto
    {
        public User Challenger { get; set; }
        public string InviteMessage { get; set; }
    }
}