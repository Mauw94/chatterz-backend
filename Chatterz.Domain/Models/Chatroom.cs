namespace Chatterz.Domain.Models
{
    public class Chatroom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }

        public Chatroom()
        {
            Name = Guid.NewGuid().ToString();
        }
    }
}