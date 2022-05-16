using Microsoft.Extensions.Caching.Distributed;

namespace Cache.Services.Redis
{
    public class RedisCache : IRedisCache
    {
        private readonly IDistributedCache _cache;
        public RedisCache(IDistributedCache cache)
        {
            _cache = cache;
        }
        public async Task<byte[]> Get(string key)
        {
            return await _cache.GetAsync(key);
        }

        public async Task Set(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            await _cache.SetAsync(key, value, options);
        }
    }
}
