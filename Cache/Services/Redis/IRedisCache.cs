using Microsoft.Extensions.Caching.Distributed;

namespace Cache.Services.Redis
{
    public interface IRedisCache
    {
        Task<byte[]> Get(string key);
        Task Set(string key, byte[] value, DistributedCacheEntryOptions options);
    }
}
