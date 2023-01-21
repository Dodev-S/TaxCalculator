using FluentValidation;
using TaxCalculator.Core.Calculations;

namespace TaxCalculator.Api.Validators;

public class TaxPayerValidator : AbstractValidator<TaxPayer>
{
    public TaxPayerValidator()
    {
        this.RuleFor(x => x.FullName).Matches(@"^[A-Za-z]+[\s][A-Za-z\s]+$").WithMessage("FullName must be at least two words separated by space.");
        this.RuleFor(x => x.Ssn).MinimumLength(5).WithMessage("Ssn must be at least 5 digits.");
        this.RuleFor(x => x.Ssn).MaximumLength(10).WithMessage("Ssn must be less than or equal to 10 digits.");
        this.RuleFor(x => x.Ssn).Matches(@"^[0-9]+$").WithMessage("Ssn must be only digits.");
        this.RuleFor(x => x.GrossIncome).NotNull();
    }
}
