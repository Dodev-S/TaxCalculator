using Microsoft.Extensions.Options;
using TaxCalculator.Core.Calculations;
using TaxCalculator.Core.Calculations.TaxRules;
using TaxCalculator.Domain.Calculations;
using Xunit;

namespace TaxCalculator.UnitTests.TaxRules;

public class TaxThresholdRuleTests
{
    private readonly TaxesConfig _config
    = Options.Create(new TaxesConfig { NontaxableThreshold = 1000, IncomeTaxRate = 0.1m, SocialTaxHighLimit = 3000m, SocialTaxRate = 0.15m, CharityRateLimit = 0.1m }).Value;

    [Fact]
    public void Execute_BelowThreshold_IsCorrect()
    {
        var taxThresholdRule = new TaxThresholdRule();
        var taxableIncome = 1000m;
        var taxPayer = new TaxPayer("John Smith", null, taxableIncome, "12345", 0);
        var taxes = new Taxes(taxableIncome, taxableIncome, 0, 0, 0);

        taxThresholdRule.Execute(taxes, taxPayer, _config);

        Assert.Equal(0, taxes.TaxableIncome);
    }

    [Fact]
    public void Execute_AboveThreshold_IsCorrect()
    {
        var taxThresholdRule = new TaxThresholdRule();
        var taxableIncome = 4000m;
        var taxPayer = new TaxPayer("John Smith", null, taxableIncome, "12345", 0);
        var taxes = new Taxes(taxableIncome, taxableIncome, 0, 0, 0);

        taxThresholdRule.Execute(taxes, taxPayer, _config);

        Assert.Equal(taxableIncome - _config.NontaxableThreshold, taxes.TaxableIncome);
    }
}
