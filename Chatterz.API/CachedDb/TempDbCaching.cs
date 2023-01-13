namespace Chatterz.API.CachedDb 
{
    public abstract class TempDbCaching<T> : ITempDbCaching<T> where T : class
    {
        private DateTime _expirationTime;
        private readonly List<T> _cache = new();
        private readonly Dictionary<T, List<T>> _tempDb = new(); // key: chatrooms | value: connected users
        
        public virtual void Save(T chatroomId, T user)
        {
            if (_tempDb.ContainsKey(chatroomId))
                _tempDb[chatroomId].Add(user);
            else
                _tempDb.TryAdd(chatroomId, new List<T> { user });
        }

        public virtual List<T> ConnectedUsers(T chatroomId)
        {
            if (_tempDb.TryGetValue(chatroomId, out var connectedUsers))
                return connectedUsers;
            else
                throw new ArgumentException($"Key was not found {chatroomId}");
        }

        public virtual List<T> GetAllChatrooms()
        {
            if (_tempDb.Keys.Any())
                return _tempDb.Keys.ToList();
            
            return null;
        }

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