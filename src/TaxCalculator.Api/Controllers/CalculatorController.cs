using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Core.Calculations;

namespace TaxCalculator.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CalculatorController : ControllerBase
{
    private readonly ITaxCalculationsHandler _taxCalculationsHandler;
    private readonly IValidator<TaxPayer> _validator;

    public CalculatorController(ITaxCalculationsHandler taxCalculationsHandler, IValidator<TaxPayer> validator)
    {
        _taxCalculationsHandler = taxCalculationsHandler;
        _validator = validator;
    }

    [HttpPost("Calculate")]
    public async Task<IActionResult> Calculate([FromBody] TaxPayer taxPayer)
    {
        ValidationResult result = await _validator.ValidateAsync(taxPayer);

        if (!result.IsValid)
        {
            return BadRequest(result);
        }

        return Ok(_taxCalculationsHandler.Execute(taxPayer));
    }
}