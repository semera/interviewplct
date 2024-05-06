using Api.Domain.Configs;
using Api.Domain.Entities;
using Api.Domain.Tools;

namespace Api.Domain.Rules;

/// <summary>
/// employees have a base cost of $1,000 per month (for benefits)
/// </summary>
public class BaseCostRule(IDaysCalculator daysCalculator, IRatesCalculator ratesCalculator, IConfig config)
    : DaysRateRule(daysCalculator)
{
    /// <inheritdoc/>
    protected override decimal GetDailyRate(Paycheck paycheck)
        => ratesCalculator.DailyRateFromMonth(config.BaseCostPerMonth) * Consts.Negative;

    /// <inheritdoc/>
    protected override string GetName(Paycheck paycheck)
        => "base cost";
}
