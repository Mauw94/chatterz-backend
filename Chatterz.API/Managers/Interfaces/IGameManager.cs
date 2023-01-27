namespace Chatterz.API.Manages.Interfaces
{
    public interface IGameManager
    {
        Task AddPlayerToGameGroup(string groupId, string connectionId);
    }
}