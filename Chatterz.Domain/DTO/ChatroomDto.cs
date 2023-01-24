using Chatterz.Domain.Models;

namespace Chatterz.Domain.DTO
{
    public class ChatroomDto
    {
        public string Id { get; set; }
        public List<User> Users { get; set; }
    }
}