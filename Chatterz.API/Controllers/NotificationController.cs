using Chatterz.HUBS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chatterz.API.Controllers;

[ApiController]
public class NotificationController : ControllerBase
{
    private IHubContext<NotificationHub> _hubContext;

    public NotificationController(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpGet]
    [Route("api/notification/send")]
    public async Task SendNotification()
    {
        await _hubContext.Clients.All.SendAsync("Notification", "test123");
    }
}