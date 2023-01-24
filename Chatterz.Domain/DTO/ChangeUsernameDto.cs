namespace Chatterz.Domain.DTO
{
    public class ChangeUsernameDto
    {
        public string OldUsername { get; set; }
        public string NewUsername { get; set; }
        public int UserId { get; set; }
        public int ChatroomId { get; set; }
    }
}