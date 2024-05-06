using Api.Domain.Configs;
using Api.Domain.Entities;
using Api.Domain.Tools;

namespace Api.Domain.Rules;

/// <summary>
/// employees that make more than $80,000 per year will incur an additional 2% of their yearly salary in benefits costs
/// </summary>
public class HighSalaryCostRule(IDaysCalculator daysCalculator, IRatesCalculator ratesCalculator, IConfig config)
    : DaysRateRule(daysCalculator)
{

    /// <inheritdoc/>
    public override void Apply(Paycheck paycheck, Employee _)
    {
        if (paycheck.AnnualSalary > config.HighSalaryCostLimit)
        {
            base.Apply(paycheck, _);
        }
    }

    /// <inheritdoc/>
    protected override decimal GetDailyRate(Paycheck paycheck)
        => ratesCalculator.DailyRateFromAnual(config.HighSalaryCostCoef * paycheck.AnnualSalary) * Consts.Negative;

    /// <inheritdoc/>
    protected override string GetName(Paycheck paycheck)
        => "high salary cost";
}
