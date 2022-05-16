using Cache.Services.CacheHandler;
using Cache;
using Microsoft.AspNetCore.Mvc;
using DataAccess.WebApiServices;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ICacheHandler _cacheHandler;
        private readonly ITaaghcheService _taaghcheService;
        public BookController(ICacheHandler cacheHandler, ITaaghcheService taaghcheService)
        {
            _cacheHandler = cacheHandler;
            _taaghcheService = taaghcheService;
        }

        [HttpGet("{id}")]
        public async Task<string?> Get(int id)
        {
            return await _cacheHandler.GetAsync(new CacheKey<int>("", "").Create(id),
                async () => { return await _taaghcheService.GetById(id); },
                TimeSpan.FromDays(2));

        }
    }
}