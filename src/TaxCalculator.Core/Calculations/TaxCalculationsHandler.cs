using TaxCalculator.Core.Utilities;
using TaxCalculator.Domain.Calculations;

namespace TaxCalculator.Core.Calculations;

public interface ITaxCalculationsHandler
{
    TaxesDto Execute(TaxPayer taxPayer);
}

public class TaxCalculationsHandler : ITaxCalculationsHandler
{
    private readonly ITaxCalculator _taxCalculator;
    private readonly ITaxesRepository _taxesRepository;

    public TaxCalculationsHandler(ITaxCalculator taxCalculator, ITaxesRepository taxesRepository)
    {
        _taxCalculator = taxCalculator;
        _taxesRepository = taxesRepository;
    }

    public TaxesDto Execute(TaxPayer taxPayer)
    {
        var taxes = _taxesRepository.Get(HashHelper.ComputeSha256Hash(taxPayer));
        if (taxes != null)
            return new TaxesDto(taxes);

        taxes = _taxCalculator.CalculateTaxes(taxPayer);

        _taxesRepository.Add(HashHelper.ComputeSha256Hash(taxPayer), taxes);

        return new TaxesDto(taxes);
    }
}
