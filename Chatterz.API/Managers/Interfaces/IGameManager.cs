using Chatterz.Domain.Models;

namespace Chatterz.API.Manages.Interfaces
{
    public interface IGameManager
    {
        /// <summary>
        /// Add a player to the game group, user by SignalR to send messages to.
        /// </summary>
        Task AddPlayerToGameGroup(string groupId, string connectionId);

        /// <summary>
        /// Send update to the group that a player has joined.
        /// </summary>
        Task SendGameroomUpdate(string groupId, string message);
    }
}