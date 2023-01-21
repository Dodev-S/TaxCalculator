using FakeItEasy;
using TaxCalculator.Core.Calculations;
using TaxCalculator.Domain.Calculations;
using Xunit;

namespace TaxCalculator.UnitTests;

public class TaxCalculationsHandlerTests
{
    [Fact]
    public void Execute_WithExisting_DoesNotCreateAgain()
    {
        var calculator = A.Fake<ITaxCalculator>();
        var repo = A.Fake<ITaxesRepository>();
        var taxes = new Taxes(1010, 1000, 10, 100, 150);

        A.CallTo(() => repo.Get(A<string>.Ignored))
        .Returns(taxes);

        var handler = new TaxCalculationsHandler(calculator, repo);
        handler.Execute(new TaxPayer("John Smith", null, 1010, "123456", 10));

        A.CallTo(() => calculator.CalculateTaxes(A<TaxPayer>.Ignored))
        .MustNotHaveHappened();
        A.CallTo(() => repo.Add(A<string>.Ignored, A<Taxes>.Ignored))
        .MustNotHaveHappened();
    }

    [Fact]
    public void Execute_WithNewRequest_CreatesNew()
    {
        var calculator = A.Fake<ITaxCalculator>();
        var repo = A.Fake<ITaxesRepository>();
        var taxes = new Taxes(1010, 1000, 10, 100, 150);

        A.CallTo(() => calculator.CalculateTaxes(A<TaxPayer>.Ignored))
        .Returns(taxes);
        A.CallTo(() => repo.Get(A<string>.Ignored))
        .Returns(null);
        A.CallTo(() => repo.Add(A<string>.Ignored, A<Taxes>.Ignored))
        .DoesNothing();

        var handler = new TaxCalculationsHandler(calculator, repo);
        handler.Execute(new TaxPayer("John Smith", null, 1010, "123456", 10));

        A.CallTo(() => calculator.CalculateTaxes(A<TaxPayer>.Ignored))
        .MustHaveHappenedOnceExactly();
        A.CallTo(() => repo.Add(A<string>.Ignored, A<Taxes>.Ignored))
        .MustHaveHappenedOnceExactly();
    }
}
