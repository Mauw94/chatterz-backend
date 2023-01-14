using Chatterz.API.InMemoryDb;
using Chatterz.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Chatterz.API.Controllers
{
    // TODO: temp login functionality
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUsersDb _usersDb;

        public LoginController(IUsersDb usersDb)
        {
            _usersDb = usersDb;
        }

        [HttpPost]
        [Route("api/login/login")]
        public ActionResult<bool> Login(string username, string password)
        {
            return Ok(_usersDb.Login(username, password));
        }

        [HttpPost]
        [Route("api/login/create")]
        public ActionResult<string> CreateTempUser(string username, string password)
        {
            var user = new User();

            _usersDb.SaveUser(user);

            return Ok(user.Id);
        }
    }
}