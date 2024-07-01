using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Infrastructure.Models
{
    public class TaxPayerDTO
    {
        public string FullName { get; set; }
        public string SSN { get; set; }
        public decimal GrossIncome { get; set; }
        public decimal? CharitySpent { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
