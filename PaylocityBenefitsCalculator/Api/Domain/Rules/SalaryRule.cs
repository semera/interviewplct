using Api.Domain.Entities;
using Api.Domain.Tools;

namespace Api.Domain.Rules;

/// <summary>
/// Add salary amount for a pay period.
/// </summary>
public class SalaryRule(IDaysCalculator daysCalculator, IRatesCalculator ratesCalculator)
    : DaysRateRule(daysCalculator)
{
    /// <inheritdoc/>
    protected override decimal GetDailyRate(Paycheck paycheck)
        => ratesCalculator.DailyRateFromAnual(paycheck.AnnualSalary);

    /// <inheritdoc/>
    protected override string GetName(Paycheck paycheck)
        => "base salary";
}
