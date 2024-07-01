using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Services.Validations
{
    public class Expressions
    {
        public const string NAME_REGEX_PATTERN = @"^([a-zA-Z]+( [a-zA-Z]+)+)$";
        public const string SSN_REGEX_PATTERN = @"^\d{5,10}$";
    }
}
