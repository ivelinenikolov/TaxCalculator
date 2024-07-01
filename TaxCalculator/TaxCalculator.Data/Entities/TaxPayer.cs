using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Data.Entities
{
    public class TaxPayer
    {
        public string FirstName { get; set; }
        public string SirName { get; set; }
        public string LastName { get; set; }
        public string SSN { get; set; }
        public decimal GrossIncome { get; set; }
        public decimal? CharitySpent { get; set; }
        public Tax Taxes { get; set; }
    }
}
