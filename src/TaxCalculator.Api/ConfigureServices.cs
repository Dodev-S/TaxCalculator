using FluentValidation;
using TaxCalculator.Api.Validators;
using TaxCalculator.Core.Calculations;
using TaxCalculator.Domain.Calculations;

namespace TaxCalculator.Api;

public static class ConfigureServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<ITaxCalculationsHandler, TaxCalculationsHandler>();
        services.AddTransient<ITaxCalculator, ImaginaryEuropeanUnionTaxesCalculator>();
        services.AddTransient<ITaxCalculationsHandler, TaxCalculationsHandler>();
        services.AddTransient<ITaxesRepository, TaxesRepository>();
        services.AddValidatorsFromAssemblyContaining<TaxPayerValidator>();

        return services;
    }
}
