namespace Chatterz.API.CachedDb
{
    public class UsersDbCaching : TempDbCaching<string>, IUsersDbCaching
    {
        public override void Save(string chatroomId, string user)
        {
            base.Save(chatroomId, user);
        }

        public override List<string> GetAllChatrooms()
        {
            return base.GetAllChatrooms();
        }

        public override List<string> Get(string chatroomId)
        {
            return base.Get(chatroomId);
        }
    }
}