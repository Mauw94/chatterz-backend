using Chatterz.Domain;

namespace Chatterz.API.InMemoryDb
{
    public interface IUsersDb
    {
        void SaveUser(User user);
        User GetUser(string id);
        User? Login(string username, string password);
    }
}