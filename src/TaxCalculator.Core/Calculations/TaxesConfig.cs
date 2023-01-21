namespace TaxCalculator.Core.Calculations;

public class TaxesConfig
{
    public decimal NontaxableThreshold { get; set; }
    public decimal IncomeTaxRate { get; set; }
    public decimal SocialTaxRate { get; set; }
    public decimal SocialTaxHighLimit { get; set; }
    public decimal CharityRateLimit { get; set; }
}
