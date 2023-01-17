using Microsoft.AspNetCore.Mvc;

namespace Chatterz.API.Controllers
{
    [ApiController]
    public class SignalRController : ControllerBase
    {
        [HttpPost]
        [Route("api/signalr/disconnect")]
        public ActionResult Disconnect()
        {
            return Ok();
        }
    }
}
