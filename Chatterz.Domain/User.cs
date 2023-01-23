namespace Chatterz.Domain
{
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConnectionId { get; set; }

        public User()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}