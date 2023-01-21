using Microsoft.Extensions.Options;
using TaxCalculator.Core.Calculations;
using Xunit;

namespace TaxCalculator.UnitTests;

public class ImaginaryEuropeanUnionTaxesCalculatorTests
{
    private readonly IOptions<TaxesConfig> _config
    = Options.Create(new TaxesConfig { NontaxableThreshold = 1000, IncomeTaxRate = 0.1m, SocialTaxHighLimit = 3000m, SocialTaxRate = 0.15m, CharityRateLimit = 0.1m });

    [Fact]
    public void CalculateTaxableIncome_BelowThreshold_IsCorrect()
    {
        var builder = new ImaginaryEuropeanUnionTaxesCalculator(_config);
        var taxPayer = new TaxPayer("John Smith", null, _config.Value.NontaxableThreshold, "12345", null);
        var taxes = builder.CalculateTaxes(taxPayer);

        Assert.Equal(0, taxes.TaxableIncome);
    }

    [Fact]
    public void CalculateTaxableIncome_BelowThresholdAfterCharity_IsCorrect()
    {
        var builder = new ImaginaryEuropeanUnionTaxesCalculator(_config);
        var charityAmount = 55;
        var taxPayer = new TaxPayer("John Smith", null, _config.Value.NontaxableThreshold + charityAmount, "12345", charityAmount);
        var taxes = builder.CalculateTaxes(taxPayer);

        Assert.Equal(0, taxes.TaxableIncome);
    }

    [Fact]
    public void CalculateTaxableIncome_AboveThresholdAfterCharity_IsCorrect()
    {
        var builder = new ImaginaryEuropeanUnionTaxesCalculator(_config);
        var charityAmount = 55;
        var excess = 10;
        var taxPayer = new TaxPayer("John Smith", null, _config.Value.NontaxableThreshold + charityAmount + excess, "12345", charityAmount);
        var taxes = builder.CalculateTaxes(taxPayer);

        Assert.Equal(excess, taxes.TaxableIncome);
    }

    [Fact]
    public void CalculateTaxableIncome_AboveCharityLimit_IsCorrect()
    {
        var builder = new ImaginaryEuropeanUnionTaxesCalculator(_config);
        var grossIncome = 3000m;
        var charityAmount = 310;
        var taxPayer = new TaxPayer("John Smith", null, grossIncome, "12345", charityAmount);
        var taxes = builder.CalculateTaxes(taxPayer);

        Assert.Equal(1700, taxes.TaxableIncome);
    }

    [Fact]
    public void CalculateIncomeTax_AboveThresholdAfterCharity_IsCorrect()
    {
        var builder = new ImaginaryEuropeanUnionTaxesCalculator(_config);
        var charityAmount = 55;
        var excess = 10;
        var taxPayer = new TaxPayer("John Smith", null, _config.Value.NontaxableThreshold + charityAmount + excess, "12345", charityAmount);
        var taxes = builder.CalculateTaxes(taxPayer);

        Assert.Equal(1, taxes.IncomeTax);
    }

    [Fact]
    public void CalculateSocialTax_AboveThresholdUnderLimit_IsCorrect()
    {
        var builder = new ImaginaryEuropeanUnionTaxesCalculator(_config);
        var charityAmount = 55;
        var excess = 100;
        var taxPayer = new TaxPayer("John Smith", null, _config.Value.NontaxableThreshold + charityAmount + excess, "12345", charityAmount);
        var taxes = builder.CalculateTaxes(taxPayer);

        Assert.Equal(15, taxes.SocialTax);
    }

    [Fact]
    public void CalculateSocialTax_AboveLimit_IsCorrect()
    {
        var builder = new ImaginaryEuropeanUnionTaxesCalculator(_config);
        var charityAmount = 55;
        var excess = 100;
        var taxPayer = new TaxPayer("John Smith", null, _config.Value.SocialTaxHighLimit + charityAmount + excess, "12345", charityAmount);
        var taxes = builder.CalculateTaxes(taxPayer);

        Assert.Equal(300, taxes.SocialTax);
    }
}
