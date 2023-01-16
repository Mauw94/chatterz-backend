namespace Chatterz.Domain
{
    public class ChatMessage

    {
        public string Text { get; set; }
        public string ConnectionId { get; set; }
        public DateTime DateTime { get; set; }
        public string UserName { get; set; }
        public string ChatroomId { get; set; }
    }
}