using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Data.Entities;
using TaxCalculator.Infrastructure.Models;
using TaxCalculator.Services.Interfaces;

namespace TaxCalculator.Services.Mapping
{
    public class MappingService : IMappingService
    {
        public TaxPayer MapToTaxPayer(TaxPayerDTO dto)
        {
            return new TaxPayer
            {
                FirstName = dto.FullName.Split(' ')[0],
                LastName = dto.FullName.Split(' ').Last(),
                SSN = dto.SSN,
                GrossIncome = dto.GrossIncome,
                CharitySpent = dto.CharitySpent,
                Taxes = new Tax() 
            };
        }

        public TaxDTO MapToTaxDTO(Tax tax)
        {
            return new TaxDTO
            {
                GrossIncome = tax.GrossIncome,
                CharitySpent = tax.CharitySpent,
                IncomeTax = tax.IncomeTax,
                SocialTax = tax.SocialTax,
                TotalTax = tax.TotalTax,
                NetIncome = tax.NetIncome
            };
        }
    }
}
