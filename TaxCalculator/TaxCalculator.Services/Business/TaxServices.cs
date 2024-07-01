using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using TaxCalculator.Data.Entities;
using TaxCalculator.Infrastructure.Interfaces;
using TaxCalculator.Infrastructure.Models;
using TaxCalculator.Services.Interfaces;
using TaxCalculator.Services.Settings;

public class TaxService : ITaxService
{
    private readonly TaxRulesSettings _taxRules;
    private readonly IMappingService _mappingService;
    private readonly ITaxPayerRepository _taxPayerRepository;

    public TaxService(IOptions<TaxRulesSettings> taxRules, IMappingService mappingService, ITaxPayerRepository taxPayerRepository)
    {
        _taxRules = taxRules.Value;
        _mappingService = mappingService;
        _taxPayerRepository = taxPayerRepository;
    }

    public async Task<TaxDTO> CalculateTax(TaxPayerDTO taxPayerDTO)
    {
        var cachedTaxEntity = _taxPayerRepository.GetCachedTax(taxPayerDTO);

        if (cachedTaxEntity != null)
        {
            _mappingService.MapToTaxDTO(cachedTaxEntity);
        }

        return await CalculateAndCacheTax(taxPayerDTO);
    }

    private async Task<TaxDTO> CalculateAndCacheTax(TaxPayerDTO taxPayerDTO)
    {
        var charityReduction = CalculateCharityReduction(taxPayerDTO);
        var incomeTax = CalculateIncomeTax(taxPayerDTO, charityReduction);
        var socialTax = CalculateSocialTax(taxPayerDTO, charityReduction);
        var totalTax = incomeTax + socialTax;
        var netIncome = taxPayerDTO.GrossIncome - totalTax;

        var payerEntity = _mappingService.MapToTaxPayer(taxPayerDTO);

        payerEntity.Taxes = new Tax
        {
            GrossIncome = taxPayerDTO.GrossIncome,
            CharitySpent = taxPayerDTO.CharitySpent ?? 0,
            IncomeTax = incomeTax,
            SocialTax = socialTax,
            TotalTax = totalTax,
            NetIncome = netIncome
        };

        var taxDTO = _mappingService.MapToTaxDTO(payerEntity.Taxes);

        _taxPayerRepository.AddTaxPayerAndCacheTax(taxPayerDTO, payerEntity);

        return await Task.FromResult(taxDTO);
    }

    private decimal CalculateCharityReduction(TaxPayerDTO taxpayer)
    {
        return Math.Min(taxpayer.CharitySpent ?? 0, taxpayer.GrossIncome * ( _taxRules.CharityMaxPercent / 100.0M ));
    }

    private decimal CalculateIncomeTax(TaxPayerDTO taxpayer, decimal charityReduction)
    {
        var taxableIncome = Math.Max(0, taxpayer.GrossIncome - _taxRules.NoTaxation - charityReduction);
        return taxableIncome * ( _taxRules.IncomeTaxPercent / 100.0M );
    }

    private decimal CalculateSocialTax(TaxPayerDTO taxpayer, decimal charityReduction)
    {
        var taxableBase = taxpayer.GrossIncome - charityReduction;
        var taxableAmountAfterThreshold = Math.Max(0, taxableBase - _taxRules.NoTaxation);
        var socialTaxEligibleAmount = Math.Min(2000, taxableAmountAfterThreshold); 
        return socialTaxEligibleAmount * ( _taxRules.SocialContributionPercent / 100.0M );
    }
}
