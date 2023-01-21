# TaxCalculator

This is a simple .NET 6 Web Api application which calculates taxes based on the following rules:
  1.)	There is no taxation for any amount lower or equal to 1000 Imagiaria Dollars (IDR).
  2.)	Income tax of 10% is incurred to the excess (amount above 1000).
  3.)	Social contributions of 15% are expected to be made as well. As for the previous case, the taxable income is whatever is above 1000 IDR but social contributions never apply to amounts higher than 3000.
  4.)	CharitySpent – Up to 10% of the gross income is allowed to be spent for charity causes. It then needs to be subtracted from the gross income base before the taxes are calculated.
