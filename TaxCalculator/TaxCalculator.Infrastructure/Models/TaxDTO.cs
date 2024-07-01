using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Infrastructure.Models
{
    public class TaxDTO
    {
        public decimal GrossIncome { get; set; }
        public decimal CharitySpent { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal SocialTax { get; set; }
        public decimal TotalTax { get; set; }
        public decimal NetIncome { get; set; }
    }
}
