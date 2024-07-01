using Xunit;
using Microsoft.Extensions.DependencyInjection;
using TaxCalculator.Services.Interfaces;
using TaxCalculator.Data.Entities;
using System.Threading.Tasks;
using TaxCalculator.Tests;
using TaxCalculator.Infrastructure.Models;

namespace TaxCalculator.Tests;

public class CalculationTests : IClassFixture<Startup>
{
    private readonly ITaxService _taxService;

    public CalculationTests(Startup startup)
    {
        var services = new ServiceCollection();
        startup.ConfigureServices(services);
        var serviceProvider = services.BuildServiceProvider();

        _taxService = serviceProvider.GetRequiredService<ITaxService>();
    }

    [Theory]
    [InlineData(980, 0, 0, 0, 980)]
    [InlineData(2500, 150, 135, 202.5, 2162.5)]
    [InlineData(3600, 520, 224, 300, 3076)]
    [InlineData(3400, 0, 240, 300, 2860)]
    public async Task CalculateTax_ShouldComputeCorrectly(decimal grossIncome, decimal charitySpent, decimal expectedIncomeTax, decimal expectedSocialTax, decimal expectedNetIncome)
    {
        var taxPayer = new TaxPayerDTO
        {
            GrossIncome = grossIncome,
            CharitySpent = charitySpent,
            DateOfBirth = new DateTime(2000, 1, 1),
            FullName = "John Doe",
            SSN = "123456"
        };

        var result = await _taxService.CalculateTax(taxPayer);

        Assert.NotNull(result);
        Assert.Equal(expectedIncomeTax, result.IncomeTax);
        Assert.Equal(expectedSocialTax, result.SocialTax);
        Assert.Equal(expectedNetIncome, result.NetIncome);
    }
}

