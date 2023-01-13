namespace Chatterz.API.CachedDb
{
    public interface ITempDbCaching<T> where T : class
    {
        void Save(T chatroomId, T user);
        List<T> ConnectedUsers(T chatroomId);
        List<T> GetAllChatrooms();

        void DoCache(IEnumerable<T> items, DateTime expirationTime);
        bool TryGetCache(out IEnumerable<T> items);
    }
}