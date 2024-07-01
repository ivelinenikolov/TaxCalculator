using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Data.Entities;
using TaxCalculator.Infrastructure.Models;

namespace TaxCalculator.Infrastructure.Interfaces
{
    public interface ITaxPayerRepository
    {
        void AddTaxPayerAndCacheTax(TaxPayerDTO taxPayerDTO, TaxPayer entity);

        Tax GetCachedTax(TaxPayerDTO taxPayer);
    }
}
