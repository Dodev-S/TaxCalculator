using TaxCalculator.Domain.Calculations;

namespace TaxCalculator.Core.Calculations;

public class TaxesDto
{
    public TaxesDto(Taxes taxes)
    {
        GrossIncome = taxes.GrossIncome;
        CharitySpent= taxes.CharitySpent;
        IncomeTax = taxes.IncomeTax!.Value;
        SocialTax = taxes.SocialTax!.Value;
        TotalTax = taxes.TotalTax!.Value;
        NetIncome = taxes.NetIncome!.Value;
    }

    public decimal GrossIncome { get; }
    public decimal? CharitySpent { get; }
    public decimal IncomeTax { get; }
    public decimal SocialTax { get; }
    public decimal TotalTax { get; }
    public decimal NetIncome { get; }
}