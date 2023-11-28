using TaxiGomelProject.Data;
using TaxiGomelProject.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TaxiGomelProject.Services
{
    public class CachedCarsService : ICachedService<Car>
    {
        private readonly TaxiGomelContext _dbContext;
        private readonly IMemoryCache _memoryCache;

        public CachedCarsService(TaxiGomelContext dbContext, IMemoryCache memoryCache)
        {
            _dbContext = dbContext;
            _memoryCache = memoryCache;
        }

        public void AddData(string cacheKey)
        {
            IEnumerable<Car> Cars = _dbContext.Cars.Include(c => c.CarModel).ToList();
            if (Cars != null)
            {
                _memoryCache.Set(cacheKey, Cars, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(296)
                });

            }

        }
        public IEnumerable<Car> GetData(string cacheKey)
        {
            IEnumerable<Car> cars;
            if (!_memoryCache.TryGetValue(cacheKey, out cars))
            {
                cars = _dbContext.Cars.Include(c => c.CarModel).ToList();
                if (cars != null)
                {
                    _memoryCache.Set(cacheKey, cars,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(296)));
                }
            }
            return cars;
        }
    }
}

