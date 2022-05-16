using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cache.Services.CacheHandler
{
    public interface ICacheHandler
    {
        Task<string?> GetAsync(string key, Func<Task<string?>> retrieveFromDataStore, TimeSpan ttl);
    }
}
