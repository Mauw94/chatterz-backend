namespace Chatterz.Domain.DTO
{
    public class WordGuesserSendDto
    {
        public string GuessedWord { get; set; }
        public string GameroomId { get; set; }
        public string ConnectionId { get; set; }
        public int PlayerId { get; set; }
        public bool PlayerTurn { get; set; }
    }
}