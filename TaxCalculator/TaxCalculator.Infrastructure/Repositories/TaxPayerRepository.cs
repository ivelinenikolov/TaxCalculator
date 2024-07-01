using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TaxCalculator.Data.Entities;
using TaxCalculator.Infrastructure.Interfaces;
using TaxCalculator.Infrastructure.Models;

namespace TaxCalculator.Infrastructure.Repositories
{
    public class TaxPayerRepository : ITaxPayerRepository
    {
        private readonly IMemoryCache _memoryCache;

        public TaxPayerRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void AddTaxPayerAndCacheTax(TaxPayerDTO taxPayerDTO, TaxPayer entity)
        {
            if (taxPayerDTO == null)
            {
                throw new ArgumentNullException("TaxPayer cannot be null.");
            }

            string cacheKeyTaxData = GenerateCacheKey(taxPayerDTO, "tax");

            _memoryCache.Set(cacheKeyTaxData, entity, new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromHours(24)
            });
        }

        public Tax GetCachedTax(TaxPayerDTO taxPayer)
        {
            if (taxPayer == null)
            {
                throw new ArgumentNullException(nameof(taxPayer), "TaxPayer cannot be null.");
            }

            string cacheKey = GenerateCacheKey(taxPayer, "tax");
            if (_memoryCache.TryGetValue(cacheKey, out TaxPayer cachedTaxData))
            {
                return cachedTaxData.Taxes;
            }

            return null;
        }

        private string GenerateCacheKey(TaxPayerDTO taxPayer, string type)
        {
            return $"Tax_{type}_{taxPayer.SSN}_{taxPayer.GrossIncome}_{taxPayer.CharitySpent}";
        }
    }
}
