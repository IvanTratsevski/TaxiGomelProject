using TaxiGomelProject.Data;
using TaxiGomelProject.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TaxiGomelProject.Services
{
    public class CachedRatesService : ICachedService<Rate>
    {
        private readonly TaxiGomelContext _dbContext;
        private readonly IMemoryCache _memoryCache;

        public CachedRatesService(TaxiGomelContext dbContext, IMemoryCache memoryCache)
        {
            _dbContext = dbContext;
            _memoryCache = memoryCache;
        }

        public void AddData(string cacheKey)
        {
            IEnumerable<Rate> rates = _dbContext.Rates.ToList();
            if (rates != null)
            {
                _memoryCache.Set(cacheKey, rates, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(296)
                });

            }

        }
        public IEnumerable<Rate> GetData(string cacheKey)
        {
            IEnumerable<Rate> rates;
            if (!_memoryCache.TryGetValue(cacheKey, out rates))
            {
                rates = _dbContext.Rates.ToList();
                if (rates != null)
                {
                    _memoryCache.Set(cacheKey, rates,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(296)));
                }
            }
            return rates;
        }
    }
}

