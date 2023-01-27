namespace Chatterz.Domain.Models
{
    public class WordGuesser : Game
    {
        public string WordToGuess { get; set; } = "TEST";
        public int AmountOfGuesses { get; set; }
    }
}