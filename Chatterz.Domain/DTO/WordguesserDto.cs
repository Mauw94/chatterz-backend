namespace Chatterz.Domain.DTO
{
    public class WordGuesserDto
    {
        public string WordToGuess { get; set; }
        public string GuessedWord { get; set; }
        public string GameroomId { get; set; }
        public int PlayerToPlay { get; set; }
        public List<int> PlayerIds { get; set; }
    }
}