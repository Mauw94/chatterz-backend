using Chatterz.API.InMemoryDb;
using Chatterz.Domain;
using Chatterz.Domain.DTO;
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
        public ActionResult ChangeUsername(ChangeUsernameDto dto)
        {
            _usersDb.ChangeUsername(dto.NewUsername, dto.UserId);
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