using Chatterz.API.InMemoryDb;
using Chatterz.Domain;
using Chatterz.Domain.DTO;
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

        [HttpGet]
        [Route("api/login/new")] // test/debug method
        public ActionResult<User> FetchTestUser()
        {
            return Ok(_usersDb.FetchTestUser());
        }

        [HttpPost]
        [Route("api/login/login")]
        public ActionResult<User> Login(UserLoginDto userLogin)
        {
            var user = _usersDb.Login(userLogin.UserName, userLogin.Password);
            if (user == null)
                return BadRequest("Login credentials do not match.");

            return Ok(user);
        }

        [HttpPost]
        [Route("api/login/create")]
        public ActionResult<string> CreateTempUser(UserLoginDto userLogin)
        {
            var user = new User();
            user.UserName = userLogin.UserName;
            user.Password = userLogin.Password;

            _usersDb.SaveUser(user);

            return Ok(user.Id);
        }
    }
}