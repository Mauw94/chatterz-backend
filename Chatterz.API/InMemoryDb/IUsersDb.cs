using Chatterz.Domain;

namespace Chatterz.API.InMemoryDb
{
    public interface IUsersDb
    {
        User FetchTestUser();
        void SaveUser(User user);
        User GetUser(string id);
        User? Login(string username, string password);
        void ChangeUsername(string newUsername, string userId);
        List<User> GetAll();
    }
}