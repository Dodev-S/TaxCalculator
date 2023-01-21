using TaxCalculator.Domain.Calculations;

namespace TaxCalculator.Core.Calculations.TaxRules;

public class SocialTaxRule : ITaxRule
{
    public void Execute(Taxes taxes, TaxPayer taxPayer, TaxesConfig taxesConfig)
    {
        taxes.SocialTax = taxesConfig.SocialTaxRate * Math.Min(taxes.TaxableIncome, taxesConfig.SocialTaxHighLimit - taxesConfig.NontaxableThreshold);
    }
}
