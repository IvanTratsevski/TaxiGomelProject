using TaxiGomelProject.Data;
using TaxiGomelProject.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TaxiGomelProject.Services
{
    public class CachedCarModelsService : ICachedService<CarModel>
    {
        private readonly TaxiGomelContext _dbContext;
        private readonly IMemoryCache _memoryCache;

        public CachedCarModelsService(TaxiGomelContext dbContext, IMemoryCache memoryCache)
        {
            _dbContext = dbContext;
            _memoryCache = memoryCache;
        }

        public void AddData(string cacheKey)
        {
            IEnumerable<CarModel> car_models = _dbContext.CarModels.ToList();
            if (car_models != null)
            {
                _memoryCache.Set(cacheKey, car_models, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(296)
                });

            }

        }
        public IEnumerable<CarModel> GetData(string cacheKey)
        {
            IEnumerable<CarModel> car_models;
            if (!_memoryCache.TryGetValue(cacheKey, out car_models))
            {
                car_models = _dbContext.CarModels.ToList();
                if (car_models != null)
                {
                    _memoryCache.Set(cacheKey, car_models,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(296)));
                }
            }
            return car_models;
        }
    }
}

