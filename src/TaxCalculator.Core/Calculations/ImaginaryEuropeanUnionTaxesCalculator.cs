using Microsoft.Extensions.Options;
using TaxCalculator.Core.Calculations.TaxRules;

namespace TaxCalculator.Core.Calculations;

public class ImaginaryEuropeanUnionTaxesCalculator : TaxCalculator
{
    public ImaginaryEuropeanUnionTaxesCalculator(IOptions<TaxesConfig> taxesConfig) : base(taxesConfig)
    {
        _taxRules.Push(new SocialTaxRule());
        _taxRules.Push(new IncomeTaxRule());
        _taxRules.Push(new CharityRule());
        _taxRules.Push(new TaxThresholdRule());
    }
}