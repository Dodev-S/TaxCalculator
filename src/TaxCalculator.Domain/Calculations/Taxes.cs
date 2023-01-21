namespace TaxCalculator.Domain.Calculations;

public class Taxes
{
    public Taxes(decimal grossIncome, decimal taxableIncome, decimal? charitySpent, decimal? incomeTax, decimal? socialTax)
    {
        GrossIncome = grossIncome;
        TaxableIncome = taxableIncome;
        CharitySpent = charitySpent;
        IncomeTax = incomeTax;
        SocialTax = socialTax;
    }

    public decimal GrossIncome { get; set; }
    public decimal TaxableIncome { get; set; }
    public decimal? CharitySpent { get; set; }
    public decimal? IncomeTax { get; set; }
    public decimal? SocialTax { get; set; }
    public decimal? TotalTax { get => IncomeTax + SocialTax; }
    public decimal? NetIncome { get => GrossIncome - TotalTax; }
}