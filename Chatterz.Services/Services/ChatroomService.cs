using Chatterz.DataAccess.Interfaces;
using Chatterz.Domain.Models;
using Chatterz.Services.Interfaces;

namespace Chatterz.Services.Services
{
    public class ChatroomService : Service<Chatroom>, IChatroomService
    {
        private readonly IChatroomRepository _chatroomRepo;

        public ChatroomService(IRepository<Chatroom> repo, IChatroomRepository chatroomRepo)
            : base(repo)
        {
            _chatroomRepo = chatroomRepo;
        }
    }
}