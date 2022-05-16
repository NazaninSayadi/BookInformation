using Microsoft.Extensions.Caching.Memory;

namespace Cache.Services.MemoryCache
{
    public class InMemoryCache : IInMemoryCache
    {
        private readonly IMemoryCache _memoryCache;

        public InMemoryCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public byte[] Get(string key)
        {
            return _memoryCache.TryGetValue(key, out byte[] cachedItem) ? cachedItem : default;
        }
        public void Set<T>(string key, T value, MemoryCacheEntryOptions options)
        {
             _memoryCache.Set(key,value,options);
        }
    }
}
