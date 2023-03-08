using Chatterz.HUBS.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Chatterz.HUBS;

public class NotificationHub : Hub<INotificationHub>
{
    public async Task SendAsync()
    {
        await Clients.All.SendNotification("test123");
    }
}