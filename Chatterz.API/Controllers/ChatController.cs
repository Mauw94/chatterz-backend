using Chatterz.Domain;
using Chatterz.HUBS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chatterz.API.Controllers
{
    [ApiController]
    public class ChatController : ControllerBase
    {
        private IHubContext<ChatHub> _hubContext;

        public ChatController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        [Route("api/chat/send")]
        public async Task SendMessage(ChatMessage message)
        {
            await _hubContext.Clients.All.SendAsync("messageReceivedFromApi", message);
        }

    }
}
