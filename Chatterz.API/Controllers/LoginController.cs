using Microsoft.AspNetCore.Mvc;

namespace Chatterz.API.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        public LoginController()
        {
        }

        [HttpPost]
        [Route("api/login/login")]
        public ActionResult Login(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}