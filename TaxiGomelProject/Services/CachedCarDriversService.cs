using TaxiGomelProject.Data;
using TaxiGomelProject.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TaxiGomelProject.Services
{
    public class CachedCarDriversService : ICachedService<CarDriver>
    {
        private readonly TaxiGomelContext _dbContext;
        private readonly IMemoryCache _memoryCache;

        public CachedCarDriversService(TaxiGomelContext dbContext, IMemoryCache memoryCache)
        {
            _dbContext = dbContext;
            _memoryCache = memoryCache;
        }

        public void AddData(string cacheKey)
        {
            IEnumerable<CarDriver> CarDrivers = _dbContext.CarDrivers.Include(c => c.Car).Include(c => c.Driver).ToList();
            if (CarDrivers != null)
            {
                _memoryCache.Set(cacheKey, CarDrivers, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(296)
                });

            }

        }
        public IEnumerable<CarDriver> GetData(string cacheKey)
        {
            IEnumerable<CarDriver> cardrivers;
            if (!_memoryCache.TryGetValue(cacheKey, out cardrivers))
            {
                cardrivers = _dbContext.CarDrivers.Include(c => c.Car).Include(c => c.Driver).ToList();
                if (cardrivers != null)
                {
                    _memoryCache.Set(cacheKey, cardrivers,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(296)));
                }
            }
            return cardrivers;
        }
    }
}

