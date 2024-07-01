using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Data.Entities;
using TaxCalculator.Infrastructure.Models;

namespace TaxCalculator.Services.Interfaces
{
    public interface IMappingService
    {
        TaxPayer MapToTaxPayer(TaxPayerDTO dto);
        TaxDTO MapToTaxDTO(Tax tax);
    }
}
