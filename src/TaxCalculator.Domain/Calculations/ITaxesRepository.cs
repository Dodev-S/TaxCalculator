namespace TaxCalculator.Domain.Calculations;

public interface ITaxesRepository
{
    void Add(string key, Taxes taxes);
    Taxes? Get(string key);
}