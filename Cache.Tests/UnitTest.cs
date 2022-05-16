using Cache.Services.CacheHandler;
using Cache.Services.MemoryCache;
using Cache.Services.Redis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Cache.Tests
{
    public class UnitTest
    {
        private readonly CacheHandler _mockCacheHandler;
        private readonly Mock<IInMemoryCache> _mockInMemoryCache;
        private readonly Mock<IRedisCache> _mockRedisCache;
        public UnitTest()
        {
            _mockInMemoryCache = new Mock<IInMemoryCache>();
            _mockRedisCache = new Mock<IRedisCache>();
            _mockCacheHandler = new CacheHandler(_mockInMemoryCache.Object, _mockRedisCache.Object);

        }
        [Fact]
        public async void IfMemCacheAndRedisDontHaveValue_GetFromSpecifiedFunctionAndSetInBothCaches()
        {
            var key = "123";
            var result = "{\"Book\":{\"id\"}:123}";

            var bookResult = await _mockCacheHandler.GetAsync(key, async () => { return await Task.FromResult(result); }, System.TimeSpan.FromMinutes(2));

            _mockInMemoryCache.Verify(x => x.Get(It.IsAny<string>()), Times.Once);
            _mockRedisCache.Verify(x => x.Get(It.IsAny<string>()), Times.Once);
            _mockInMemoryCache.Verify(x => x.Set(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<MemoryCacheEntryOptions>()), Times.Once);
            _mockRedisCache.Verify(x => x.Set(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<DistributedCacheEntryOptions>()), Times.Once);

            Assert.Equal(result, bookResult);

        }
        [Fact]
        public async void IfMemCacheHasValue_GetResultFromMemCache()
        {
            var key = "123";
            var result = "{\"book\":123}";
            _mockInMemoryCache.Setup(x => x.Get(key)).Returns(JsonSerializer.SerializeToUtf8Bytes(result, typeof(string)));

            var bookResult = await _mockCacheHandler.GetAsync(key, async () => { return await Task.FromResult(result); }, System.TimeSpan.FromMinutes(2));

            _mockInMemoryCache.Verify(x => x.Get(It.IsAny<string>()), Times.Once);
            Assert.Equal(result, bookResult);
        }
        [Fact]
        public async void IfRedisHasValue_GetResultFromRedis()
        {
            var key = "123";
            var result = "{\"book\":123}";
            _mockRedisCache.Setup(x => x.Get(key)).ReturnsAsync(JsonSerializer.SerializeToUtf8Bytes(result, typeof(string)));

            var bookResult = await _mockCacheHandler.GetAsync(key, async () => { return await Task.FromResult(result); }, System.TimeSpan.FromMinutes(2));

            _mockRedisCache.Verify(x => x.Get(It.IsAny<string>()), Times.Once);
            Assert.Equal(result, bookResult);
        }
    }
}