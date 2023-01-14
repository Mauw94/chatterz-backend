namespace Chatterz.API.Caching 
{
    public abstract class CachingProvider<T> : ICachingProvider<T> where T : class
    {
        private DateTime _expirationTime;
        private readonly List<T> _cache = new();
        
        public virtual void DoCache(IEnumerable<T> items, DateTime expirationTime)
        {
            _cache.Clear();
            _cache.AddRange(items);
            _expirationTime = expirationTime;
        }

        public virtual bool TryGetCache(out IEnumerable<T> items)
        {
            items = _cache;
            return DateTime.Now < _expirationTime;
        }
    }
}