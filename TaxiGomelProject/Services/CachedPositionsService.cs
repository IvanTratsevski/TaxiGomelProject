using TaxiGomelProject.Data;
using TaxiGomelProject.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TaxiGomelProject.Services
{
    public class CachedPositionsService : ICachedService<Position>
    {
        private readonly TaxiGomelContext _dbContext;
        private readonly IMemoryCache _memoryCache;

        public CachedPositionsService(TaxiGomelContext dbContext, IMemoryCache memoryCache)
        {
            _dbContext = dbContext;
            _memoryCache = memoryCache;
        }

        public void AddData(string cacheKey)
        {
            IEnumerable<Position> positions = _dbContext.Positions.ToList();
            if (positions != null)
            {
                _memoryCache.Set(cacheKey, positions, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(296)
                });

            }

        }
        public IEnumerable<Position> GetData(string cacheKey)
        {
            IEnumerable<Position> positions;
            if (!_memoryCache.TryGetValue(cacheKey, out positions))
            {
                positions = _dbContext.Positions.ToList();
                if (positions != null)
                {
                    _memoryCache.Set(cacheKey, positions,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(296)));
                }
            }
            return positions;
        }
    }
}

