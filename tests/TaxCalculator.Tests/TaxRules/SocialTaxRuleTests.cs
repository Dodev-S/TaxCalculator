using Microsoft.Extensions.Options;
using TaxCalculator.Core.Calculations;
using TaxCalculator.Core.Calculations.TaxRules;
using TaxCalculator.Domain.Calculations;
using Xunit;

namespace TaxCalculator.UnitTests.TaxRules;

public class SocialTaxRuleTests
{
    private readonly TaxesConfig _config
    = Options.Create(new TaxesConfig { NontaxableThreshold = 1000, IncomeTaxRate = 0.1m, SocialTaxHighLimit = 3000m, SocialTaxRate = 0.15m, CharityRateLimit = 0.1m }).Value;

    [Fact]
    public void Execute_BelowHighLimit_IsCorrect()
    {
        var socialTaxRule = new SocialTaxRule();
        var taxableIncome = 1000m;
        var taxPayer = new TaxPayer("John Smith", null, taxableIncome, "12345", 0);
        var taxes = new Taxes(taxableIncome, taxableIncome, 0, 0, 0);

        socialTaxRule.Execute(taxes, taxPayer, _config);

        Assert.Equal(150, taxes.SocialTax);
    }

    [Fact]
    public void Execute_AboveHighLimit_IsCorrect()
    {
        var socialTaxRule = new SocialTaxRule();
        var taxableIncome = 4000m;
        var taxPayer = new TaxPayer("John Smith", null, taxableIncome, "12345", 0);
        var taxes = new Taxes(taxableIncome, taxableIncome, 0, 0, 0);

        socialTaxRule.Execute(taxes, taxPayer, _config);

        Assert.Equal(300, taxes.SocialTax);
    }
}
