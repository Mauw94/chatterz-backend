namespace Chatterz.Domain.DTO
{
    public class ChatroomJoinDto
    {
        public int ChatroomId { get; set; }
        public int UserId { get; set; }
        public string ConnectionId { get; set; }
    }
}