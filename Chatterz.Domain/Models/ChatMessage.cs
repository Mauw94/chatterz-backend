namespace Chatterz.Domain.Models
{
    public class ChatMessage

    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string ConnectionId { get; set; }
        public DateTime DateTime { get; set; }
        public string UserName { get; set; }
        public int ChatroomId { get; set; }
    }
}