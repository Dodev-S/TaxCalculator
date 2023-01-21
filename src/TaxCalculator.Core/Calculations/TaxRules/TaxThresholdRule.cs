using TaxCalculator.Domain.Calculations;

namespace TaxCalculator.Core.Calculations.TaxRules;

public class TaxThresholdRule : ITaxRule
{
    public void Execute(Taxes taxes, TaxPayer taxPayer, TaxesConfig taxesConfig)
    {
        taxes.TaxableIncome = Math.Max(taxes.TaxableIncome - taxesConfig.NontaxableThreshold, 0);
    }
}
