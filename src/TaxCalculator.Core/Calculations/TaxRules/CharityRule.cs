using TaxCalculator.Domain.Calculations;

namespace TaxCalculator.Core.Calculations.TaxRules;

public class CharityRule : ITaxRule
{
    public void Execute(Taxes taxes, TaxPayer taxPayer, TaxesConfig taxesConfig)
    {
        var accountableCharity = Math.Min(taxPayer.CharitySpent.HasValue ? taxPayer.CharitySpent.Value : 0m, taxesConfig.CharityRateLimit * taxPayer.GrossIncome!.Value);
        taxes.TaxableIncome = Math.Max(taxes.TaxableIncome - accountableCharity, 0);
    }
}
