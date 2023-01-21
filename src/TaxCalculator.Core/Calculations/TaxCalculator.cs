using Microsoft.Extensions.Options;
using TaxCalculator.Core.Calculations.TaxRules;
using TaxCalculator.Domain.Calculations;

namespace TaxCalculator.Core.Calculations;

public interface ITaxCalculator
{
    Taxes CalculateTaxes(TaxPayer taxPayer);
}

public abstract class TaxCalculator : ITaxCalculator
{
    protected Stack<ITaxRule> _taxRules = new Stack<ITaxRule>();
    private readonly TaxesConfig _taxesConfig;

    public TaxCalculator(IOptions<TaxesConfig> taxesConfig)
    {
        _taxesConfig = taxesConfig.Value;
    }

    public Taxes CalculateTaxes(TaxPayer taxPayer)
    {
        var taxes = new Taxes(taxPayer.GrossIncome!.Value, taxPayer.GrossIncome!.Value, taxPayer.CharitySpent, 0, 0);

        foreach (var taxRule in _taxRules)
        {
            taxRule.Execute(taxes, taxPayer, _taxesConfig);
        }

        return taxes;
    }
}