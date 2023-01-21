namespace TaxCalculator.Core.Calculations;

public record TaxPayer(string FullName, DateTime? DateOfBirth, decimal? GrossIncome, string Ssn, decimal? CharitySpent);
