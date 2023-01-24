using Chatterz.DataAccess.Interfaces;
using Chatterz.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Chatterz.DataAccess.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository() { }

        public UserRepository(ApplicationDbContext context) : base(context) { }

        public async Task<User> Login(string username, string password)
        {
            var user = await ApplicationDbContext.Users
                .FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);

            return user;
        }

        public async Task UpdateConnectionInfo(int id, string connectionId)
        {
            var user = await ApplicationDbContext.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            user.ConnectionId = connectionId;

            ApplicationDbContext.Update(user);
            await ApplicationDbContext.SaveChangesAsync();
        }
    }
}