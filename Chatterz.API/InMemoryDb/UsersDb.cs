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
            _users.Add(new User
            {
                UserName = "123",
                Password = "test"
            });
        }

        public User FetchTestUser() // Debug/test method
        {
            return _users.First();
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

        public void ChangeUsername(string newUsername, string userId)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                throw new ArgumentException($"User with id {userId} does not exists.");


            user.UserName = newUsername;
        }

        public List<User> GetAll() => _users.ToList();
        public void SaveUser(User user) => _users.Add(user);
    }
}