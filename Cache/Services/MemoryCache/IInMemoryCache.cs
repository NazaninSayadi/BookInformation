using Microsoft.Extensions.Caching.Memory;

namespace Cache.Services.MemoryCache
{
    public interface IInMemoryCache
    {
        byte[] Get(string key);
        void Set<T>(string key,T value, MemoryCacheEntryOptions options);
    }
}
