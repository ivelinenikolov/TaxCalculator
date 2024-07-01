using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Infrastructure.Models;

namespace TaxCalculator.Services.Validations
{
    public class TaxPayerDTOValidator : AbstractValidator<TaxPayerDTO>
    {
        public TaxPayerDTOValidator()
        {
            RuleFor(taxPayer => taxPayer.FullName)
                .NotEmpty().WithMessage(Messages.NAME_NOT_NULL)
                .Matches(Expressions.NAME_REGEX_PATTERN).WithMessage(Messages.NAME_REQUIREMENTS);

            RuleFor(taxPayer => taxPayer.SSN)
                .NotEmpty().WithMessage(Messages.SSN_NOT_NULL)
                .Length(5, 10).WithMessage(Messages.SSN_LENGTH)
                .Matches(Expressions.SSN_REGEX_PATTERN).WithMessage(Messages.SSN_ONLY_NUMBER);

            RuleFor(taxPayer => taxPayer.GrossIncome)
                .GreaterThan(0).WithMessage(Messages.GROSS_INCOME_REQUIREMENTS);

            RuleFor(taxPayer => taxPayer.CharitySpent)
                .GreaterThanOrEqualTo(0).WithMessage(Messages.CHARITY_REQUIREMENTS);
        }
    }
}
