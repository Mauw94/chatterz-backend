namespace Chatterz.HUBS.Interfaces
{
    public interface IGameHub
    {
        Task GameUpdateReceievedFromHub(string guessedWord);
    }
}