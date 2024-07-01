using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Services.Settings
{
    public class TaxRulesSettings
    {
        public int NoTaxation { get; set; }
        public int IncomeTaxPercent { get; set; }
        public int SocialContributionPercent { get; set; }
        public int SocialContributionsMaxIncome { get; set; }
        public int CharityMaxPercent { get; set; }
    }
}
