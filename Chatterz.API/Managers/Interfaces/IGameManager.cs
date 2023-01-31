using Chatterz.Domain.DTO;
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

        /// <summary>
        /// Send update if we can start the game or not.
        /// </summary>
        Task CanStartGame(string groupId, bool canStart);

        /// <summary>
        /// Send update that the game started.
        /// </summary>
        Task StartGame(string groupId, WordGuesserDto dto);
    }
}