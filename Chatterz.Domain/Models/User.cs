namespace Chatterz.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? ConnectionId { get; set; }
        public string? GameConnectionId { get; set; }
        public int? ChatroomId { get; set; }
        public int? WordGuesserId { get; set; }
        public bool IsLoggedIn { get; set; } = false;
    }
}