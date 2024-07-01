using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Services.Validations
{
    public class Messages
    {
        public const string NAME_NOT_NULL = "Full name cannot be null.";
        public const string SSN_NOT_NULL = "Social security number cannot be null.";
        public const string SSN_LENGTH = "Social security number must be between 5 and 10 digits.";
        public const string SSN_ONLY_NUMBER = "Social security number must be a number.";
        public const string NAME_REQUIREMENTS = "Full name must contain at least two words with spaces.";
        public const string GROSS_INCOME_REQUIREMENTS = "Gross Income must be greater than 0.";
        public const string CHARITY_REQUIREMENTS = "Charity Spent must not be negative.";

    }
}
