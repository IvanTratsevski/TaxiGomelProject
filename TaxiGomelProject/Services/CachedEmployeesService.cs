using TaxiGomelProject.Data;
using TaxiGomelProject.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TaxiGomelProject.Services
{
    public class CachedEmployeesService : ICachedService<Employee>
    {
        private readonly TaxiGomelContext _dbContext;
        private readonly IMemoryCache _memoryCache;

        public CachedEmployeesService(TaxiGomelContext dbContext, IMemoryCache memoryCache)
        {
            _dbContext = dbContext;
            _memoryCache = memoryCache;
        }

        public void AddData(string cacheKey)
        {
            IEnumerable<Employee> employees = _dbContext.Employees.ToList();
            if (employees != null)
            {
                _memoryCache.Set(cacheKey, employees, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(296)
                });

            }

        }
        public IEnumerable<Employee> GetData(string cacheKey)
        {
            IEnumerable<Employee> employees;
            if (!_memoryCache.TryGetValue(cacheKey, out employees))
            {
                employees = _dbContext.Employees.ToList();
                if (employees != null)
                {
                    _memoryCache.Set(cacheKey, employees,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(296)));
                }
            }
            return employees;
        }
    }
}

