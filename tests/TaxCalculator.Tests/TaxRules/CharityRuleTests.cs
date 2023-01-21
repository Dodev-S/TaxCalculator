using Microsoft.Extensions.Options;
using TaxCalculator.Core.Calculations;
using TaxCalculator.Core.Calculations.TaxRules;
using TaxCalculator.Domain.Calculations;
using Xunit;

namespace TaxCalculator.UnitTests.TaxRules;

public class CharityRuleTests
{
    private readonly TaxesConfig _config
    = Options.Create(new TaxesConfig { NontaxableThreshold = 1000, IncomeTaxRate = 0.1m, SocialTaxHighLimit = 3000m, SocialTaxRate = 0.15m, CharityRateLimit = 0.1m }).Value;

    [Fact]
    public void Execute_BelowCharityRate_DeductsAllCharity()
    {
        var charityRule = new CharityRule();
        var grossIncome = 1000m;
        var charity = grossIncome * _config.CharityRateLimit;
        var taxPayer = new TaxPayer("John Smith", null, grossIncome, "12345", charity);
        var taxes = new Taxes(grossIncome, grossIncome, 0, 0, 0);

        charityRule.Execute(taxes, taxPayer, _config);

        Assert.Equal(grossIncome - charity, taxes.TaxableIncome);
    }

    [Fact]
    public void Execute_AboveCharityRate_DeductsCharityPartially()
    {
        var charityRule = new CharityRule();
        var grossIncome = 1000m;
        var charityExcess = 100;
        var charity = grossIncome * _config.CharityRateLimit + charityExcess;
        var taxPayer = new TaxPayer("John Smith", null, grossIncome, "12345", charity);
        var taxes = new Taxes(grossIncome, grossIncome, 0, 0, 0);

        charityRule.Execute(taxes, taxPayer, _config);

        Assert.Equal(grossIncome - (charity - charityExcess), taxes.TaxableIncome);
    }
}
