using Cache.Services.MemoryCache;
using System.Text.Json;
using Cache.Services.Redis;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace Cache.Services.CacheHandler
{
    public class CacheHandler : ICacheHandler
    {
        private readonly IInMemoryCache _memoryCache;
        private readonly IRedisCache _redisCache;

        public CacheHandler(IInMemoryCache memoryCache, IRedisCache redisCache)
        {
            _memoryCache = memoryCache;
            _redisCache = redisCache;
        }

        public async Task<string> GetAsync(string key, Func<Task<string>> retrieveFromDataStore, TimeSpan ttl)
        {
            var memcachedItem = _memoryCache.Get(key);
            if (memcachedItem is not null)
                return (string)memcachedItem;

            var redisCachedItem = await _redisCache.Get(key);
            if (redisCachedItem?.Length > 0)
            {
                var result = JsonSerializer.Deserialize<string>(new ReadOnlySpan<byte>(redisCachedItem));
                _memoryCache.Set(key, result, new MemoryCacheEntryOptions { AbsoluteExpiration = DateTime.Now.AddDays(ttl.Days), Size = 1 });
                return result;
            }

            var data = await retrieveFromDataStore();
            if (data is null || data == "null") return default;

            var serializedData = JsonSerializer.SerializeToUtf8Bytes(data);

            _memoryCache.Set(key, data, new MemoryCacheEntryOptions { AbsoluteExpiration = DateTime.Now.AddDays(ttl.Days), Size = 1 });
            await _redisCache.Set(key, serializedData, new DistributedCacheEntryOptions { AbsoluteExpiration = DateTime.Now.AddDays(ttl.Days) });

            return data;
        }
    }
}
