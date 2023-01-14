namespace Chatterz.API.Caching
{
    public interface ICachingProvider<T> where T : class
    {
        void DoCache(IEnumerable<T> items, DateTime expirationTime);
        bool TryGetCache(out IEnumerable<T> items);
    }
}