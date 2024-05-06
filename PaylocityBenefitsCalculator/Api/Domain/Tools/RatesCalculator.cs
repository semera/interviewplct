using Api.Domain.Configs;

namespace Api.Domain.Tools;

/// <inheritdoc />
/// TODO: add tests
public class RatesCalculator(IConfig config) : IRatesCalculator
{
    /// <inheritdoc />
    public decimal DailyRateFromAnual(decimal annualSalary)
    {
        return (annualSalary / config.DaysPerYear).Round();
    }

    /// <inheritdoc />
    public decimal DailyRateFromMonth(decimal monthValue)
    {
        return (monthValue * 12 / config.DaysPerYear).Round();
    }
}
