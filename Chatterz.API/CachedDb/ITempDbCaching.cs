namespace Chatterz.API.CachedDb
{
    public interface ITempDbCaching<T> where T : class
    {
        void Save(T chatroomId, T user);
        List<T> Get(T chatroomId);
        List<T> GetAllChatrooms();

        void DoCache(IEnumerable<T> items, DateTime expirationTime);
        bool TryGetCache(out IEnumerable<T> items);
    }
}