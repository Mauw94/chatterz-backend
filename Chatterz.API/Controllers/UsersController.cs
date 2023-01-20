using Chatterz.API.InMemoryDb;
using Chatterz.Domain;
using Chatterz.Domain.DTO;
using Chatterz.HUBS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chatterz.API.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUsersDb _usersDb;
        private IHubContext<ChatHub> _hubContext;
        
        public UsersController(IUsersDb usersDb, IHubContext<ChatHub> hubContext)
        {
            _usersDb = usersDb;
            _hubContext = hubContext;
        }

        [HttpPost]
        [Route("api/users/change_username")]
        public async Task<ActionResult> ChangeUsername(ChangeUsernameDto dto)
        {
            _usersDb.ChangeUsername(dto.NewUsername, dto.UserId);
            await _hubContext.Clients.Group(dto.ChatroomId).SendAsync("UsernameUpdated", dto);

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