using TaxCalculator.Domain.Calculations;

namespace TaxCalculator.Core.Calculations.TaxRules;

public interface ITaxRule
{
    void Execute(Taxes taxes, TaxPayer taxPayer, TaxesConfig taxesConfig);
}
