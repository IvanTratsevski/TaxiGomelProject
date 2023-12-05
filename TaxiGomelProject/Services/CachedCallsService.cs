using TaxiGomelProject.Data;
using TaxiGomelProject.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TaxiGomelProject.Services
{
    public class CachedCallsService : ICachedService<Call>
    {
        private readonly TaxiGomelContext _dbContext;
        private readonly IMemoryCache _memoryCache;

        public CachedCallsService(TaxiGomelContext dbContext, IMemoryCache memoryCache)
        {
            _dbContext = dbContext;
            _memoryCache = memoryCache;
        }

        public void AddData(string cacheKey)
        {
            IEnumerable<Call> calls = _dbContext.Calls.Include(c => c.Car).Include(c => c.Rate).Include(c => c.Dispatcher).ToList();
            if (calls != null)
            {
                _memoryCache.Set(cacheKey, calls, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(296)
                });

            }

        }
        public IEnumerable<Call> GetData(string cacheKey)
        {
            IEnumerable<Call> calls;
            if (!_memoryCache.TryGetValue(cacheKey, out calls))
            {
                calls = _dbContext.Calls.Include(c => c.Car).Include(c => c.Rate).Include(c => c.Dispatcher).ToList();
                if (calls != null)
                {
                    _memoryCache.Set(cacheKey, calls,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(296)));
                }
            }
            return calls;
        }
    }
}

