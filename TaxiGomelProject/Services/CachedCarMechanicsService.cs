using TaxiGomelProject.Data;
using TaxiGomelProject.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TaxiGomelProject.Services
{
    public class CachedCarMechanicsService : ICachedService<CarMechanic>
    {
        private readonly TaxiGomelContext _dbContext;
        private readonly IMemoryCache _memoryCache;

        public CachedCarMechanicsService(TaxiGomelContext dbContext, IMemoryCache memoryCache)
        {
            _dbContext = dbContext;
            _memoryCache = memoryCache;
        }

        public void AddData(string cacheKey)
        {
            IEnumerable<CarMechanic> CarMechanics = _dbContext.CarMechanics.Include(c => c.Car).Include(c => c.Mechanic).ToList();
            if (CarMechanics != null)
            {
                _memoryCache.Set(cacheKey, CarMechanics, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(296)
                });

            }

        }
        public IEnumerable<CarMechanic> GetData(string cacheKey)
        {
            IEnumerable<CarMechanic> carmechanics;
            if (!_memoryCache.TryGetValue(cacheKey, out carmechanics))
            {
                carmechanics = _dbContext.CarMechanics.Include(c => c.Car).Include(c => c.Mechanic).ToList();
                if (carmechanics != null)
                {
                    _memoryCache.Set(cacheKey, carmechanics,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(296)));
                }
            }
            return carmechanics;
        }
    }
}

