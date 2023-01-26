namespace Chatterz.Domain.Models
{
    public class WordGuesser : Game
    {
        public string WordToGuess { get; set; }
        public int AmountOfGuesses { get; set; }
    }
}