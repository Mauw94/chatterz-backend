using Chatterz.Domain.DTO;

namespace Chatterz.HUBS.Interfaces
{
    public interface IGameHub
    {
        Task GameUpdateReceievedFromHub(WordGuesserDto wordGuesserDto);
    }
}