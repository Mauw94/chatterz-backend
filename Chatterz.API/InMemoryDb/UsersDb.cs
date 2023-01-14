using Chatterz.Domain;

namespace Chatterz.API.InMemoryDb
{
    public class UsersDb : IUsersDb
    {
        private readonly List<User> _users = new();

        public bool Login(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Username == username 
                                                && u.Password == password);
            if (user == null)                                                
                return false;

            return true;
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