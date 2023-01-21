using System;
using System.Collections.Generic;
using TaxCalculator.Api.Validators;
using TaxCalculator.Core.Calculations;
using Xunit;

namespace TaxCalculator.UnitTests;

public class TaxPayerValidatorTests
{
    public static IEnumerable<object[]> ValidData =>
       new List<object[]>
       {
            new object[] {"John Smith", null, 1000m, "12345", null },
            new object[] {"John Smith Lincoln", DateTime.Now, 10m, "1234567890", null },
            new object[] {"John Smith", null, 1000m, "12345", 100m },
       };

    public static IEnumerable<object[]> InvalidData =>
       new List<object[]>
       {
            new object[] {"John", null, 1000m, "12345", null },
            new object[] {"John Smith Lincoln", DateTime.Now, null, "1234567890", null },
            new object[] {"John Smith", null, 1000m, "1234", 100m },
            new object[] {"John Smith", null, 1000m, "12345678901", 100m },
       };

    [Theory]
    [MemberData(nameof(ValidData))]
    public void ValidatePayer_WithValidData_IsValid(string fullName, DateTime? dateOfBirth, decimal? grossIncome, string ssn, decimal? charitySpent)
    {
        var validator = new TaxPayerValidator();
        var taxPayer = new TaxPayer(fullName, dateOfBirth, grossIncome, ssn, charitySpent);

        var result = validator.Validate(taxPayer);

        Assert.True(result.IsValid);
    }

    [Theory]
    [MemberData(nameof(InvalidData))]
    public void ValidatePayer_WithInvalidData_IsNotValid(string fullName, DateTime? dateOfBirth, decimal? grossIncome, string ssn, decimal? charitySpent)
    {
        var validator = new TaxPayerValidator();
        var taxPayer = new TaxPayer(fullName, dateOfBirth, grossIncome, ssn, charitySpent);

        var result = validator.Validate(taxPayer);

        Assert.True(result.IsValid == false);
    }
}