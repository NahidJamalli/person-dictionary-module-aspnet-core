using Microsoft.Extensions.Caching.Memory;
using System;

namespace PersonDictionaryModel.Core.Application.Utils
{
    public class InMemoryCache
    {
        private static MemoryCacheEntryOptions _cache { get; set; }
        public static MemoryCacheEntryOptions MemoryCacheEntryOptions { get; set; }

        static InMemoryCache()
        {
            _cache = new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddHours(1),
                Priority = CacheItemPriority.Normal,
                SlidingExpiration = TimeSpan.FromMinutes(2)
            };
        }
    }
}
