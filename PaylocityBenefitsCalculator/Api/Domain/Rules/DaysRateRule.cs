using Api.Domain.Entities;
using Api.Domain.Tools;

namespace Api.Domain.Rules;

/// <summary>
/// Base class for rules that calculate a single value based on the number of days and daily rate.
/// </summary>
public abstract class DaysRateRule(IDaysCalculator daysCalculator) : IRule
{
    /// <inheritdoc/>
    public virtual void Apply(Paycheck paycheck, Employee _)
    {
        int days = daysCalculator.Days(paycheck.Period.StartDate, paycheck.Period.EndDate);
        decimal dailyRate = GetDailyRate(paycheck);
        string name = GetName(paycheck);

        paycheck.AddItem(name, days, dailyRate);
    }

    /// <summary>
    /// Gets the name of the item visible in the paycheck.
    /// </summary>
    protected virtual string GetName(Paycheck paycheck) => GetType().Name;

    /// <summary>
    /// Calculates the daily rate.
    /// </summary>
    protected abstract decimal GetDailyRate(Paycheck paycheck);
}
