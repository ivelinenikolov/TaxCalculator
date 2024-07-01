using Xunit;
using FluentValidation.Results;
using TaxCalculator.Infrastructure.Models;
using TaxCalculator.Services.Validations;
using FluentValidation;

namespace TaxCalculator.Tests;

public class ValidationTests
{
    private readonly TaxPayerDTOValidator _validator;

    public ValidationTests()
    {
        _validator = new TaxPayerDTOValidator();
    }

    [Theory]
    [InlineData("0123", false)]
    [InlineData("012345", true)]
    [InlineData("invalidssn", false)]
    public void ValidateSSN(string ssn, bool expectedIsValid)
    {
        var taxPayerDTO = new TaxPayerDTO
        {
            SSN = ssn
        };

        ValidationResult result = _validator.Validate(taxPayerDTO, options => options.IncludeProperties(x => x.SSN));

        Assert.Equal(expectedIsValid, result.IsValid);
    }

    [Theory]
    [InlineData(10, true)]
    [InlineData(-10, false)]
    [InlineData(0, false)]
    public void ValidateGrossIncome(decimal grossIncome, bool expectedIsValid)
    {
        var taxPayerDTO = new TaxPayerDTO
        {
            GrossIncome = grossIncome
        };

        ValidationResult result = _validator.Validate(taxPayerDTO, options => options.IncludeProperties(x => x.GrossIncome));

        Assert.Equal(expectedIsValid, result.IsValid);
    }

    [Theory]
    [InlineData(10, true)]
    [InlineData(-10, false)]
    [InlineData(0, true)]
    public void ValidateCharitySpent(decimal charitySpent, bool expectedIsValid)
    {
        var taxPayerDTO = new TaxPayerDTO
        {
            CharitySpent = charitySpent
        };

        ValidationResult result = _validator.Validate(taxPayerDTO, options => options.IncludeProperties(x => x.CharitySpent));

        Assert.Equal(expectedIsValid, result.IsValid);
    }

    [Theory]
    [InlineData("", false)]
    [InlineData("John", false)]
    [InlineData("John Doe", true)]
    [InlineData("John Doe Doe", true)]
    [InlineData("John123 Doe", false)]
    public void ValidateFullName(string fullName, bool expectedIsValid)
    {
        var taxPayerDTO = new TaxPayerDTO
        {
            FullName = fullName
        };

        ValidationResult result = _validator.Validate(taxPayerDTO, options => options.IncludeProperties(x => x.FullName));

        Assert.Equal(expectedIsValid, result.IsValid);
    }
}
