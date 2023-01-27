using Chatterz.Domain.Models;

namespace Chatterz.API.Manages.Interfaces
{
    public interface IGameManager
    {
        /// <summary>
        /// Add a player to the game group.
        /// </summary>
        Task AddPlayerToGameGroup(string groupId, string connectionId);

        /// <summary>
        /// Remove a player from the game group.
        /// </summary>
        Task RemovePlayerFromGameGroup(string groupId, string connectionId);

        /// <summary>
        /// Send update to the group that a player has joined.
        /// </summary>
        Task SendGameroomUpdate(string groupId, string message);
    }
}