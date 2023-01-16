using Chatterz.API.InMemoryDb;
using Chatterz.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Chatterz.API.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUsersDb _usersDb;

        public UsersController(IUsersDb usersDb)
        {
            _usersDb = usersDb;
        }

        [HttpPost]
        [Route("api/users/change_username")]
        public ActionResult ChangeUsername(string newUsername, string userId)
        {
            _usersDb.ChangeUsername(newUsername, userId);
            return Ok();
        }

        [HttpGet]
        [Route("api/users/all")]
        public ActionResult<List<User>> GetAll()
        {
            var users = _usersDb.GetAll();
            return Ok(users);
        }
    }
}