namespace Chatterz.Domain.DTO
{
    public class ChangeUsernameDto
    {
        public string OldUsername { get; set; }
        public string NewUsername { get; set; }
        public string UserId { get; set; }
        public string ChatroomId { get; set; }
    }
}