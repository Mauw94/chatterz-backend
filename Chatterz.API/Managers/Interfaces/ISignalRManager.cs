using Chatterz.Domain.Models;

namespace Chatterz.API.Manages.Interfaces
{
    public interface ISignalRManager
    {
        /// <summary>
        /// Send update to chatroom (signalR group) when a user leaves or logs off.
        /// </summary>
        Task UpdateChatroomsOnUserLeave(User user, int chatroomId, string connectionId);

        /// <summary>
        /// Send update to chatroom (signalR group) when a user joins.
        /// </summary>
        Task UpdateChatroomsOnUserJoin(User user, int chatroomId, string connectionId);
    }
}