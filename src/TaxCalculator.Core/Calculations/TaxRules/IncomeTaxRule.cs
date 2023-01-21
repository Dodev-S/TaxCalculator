using TaxCalculator.Domain.Calculations;

namespace TaxCalculator.Core.Calculations.TaxRules;

public class IncomeTaxRule : ITaxRule
{
    public void Execute(Taxes taxes, TaxPayer taxPayer, TaxesConfig taxesConfig)
    {
        taxes.IncomeTax = taxesConfig.IncomeTaxRate * taxes.TaxableIncome;
    }
}
