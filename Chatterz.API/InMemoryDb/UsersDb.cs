using Chatterz.Domain;

namespace Chatterz.API.InMemoryDb
{
    public class UsersDb : IUsersDb
    {
        private readonly List<User> _users = new();

        public UsersDb()
        {
            _users.Add(new User
            {
                UserName = "test",
                Password = "123"
            });
        }

        public User? Login(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.UserName == username
                                                && u.Password == password);
            if (user == null)
                return null;

            return user;
        }

        public User GetUser(string id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                throw new ArgumentException($"Couldn't find user with id {id}");

            return user;
        }

        public void SaveUser(User user) => _users.Add(user);
    }
}