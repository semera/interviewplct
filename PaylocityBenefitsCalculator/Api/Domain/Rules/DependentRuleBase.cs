using System;
using Api.Domain.Entities;
using Api.Domain.Tools;

namespace Api.Domain.Rules;

/// <summary>
/// Base class for rules that apply to dependents in a paycheck.
/// </summary>
public abstract class DependentRuleBase(IDaysCalculator daysCalculator, ICalendarTools calendarTools) : IRule
{
    /// <inheritdoc/>
    public virtual void Apply(Paycheck paycheck, Employee employee)
    {
        foreach (Dependent dependent in employee.Dependents)
        {
            DateTime crucialDate = GetCrucialDate(paycheck, dependent);
            DateTime startDate = paycheck.Period.StartDate;
            DateTime endDate = paycheck.Period.EndDate;

            // is not eligible for the benefit or cost
            if (!calendarTools.IsCrucialDate(crucialDate, ref startDate, endDate))
            {
                continue;
            }

            int days = daysCalculator.Days(startDate, endDate);
            decimal dailyRate = GetDailyRate(paycheck);
            string name = GetName(paycheck, dependent);

            paycheck.AddItem(name, days, dailyRate);
        }
    }


    /// <summary>
    /// Gets the crucial date when the rule affects the paycheck - when the dependent is eligible for the benefit or cost.
    /// </summary>
    protected abstract DateTime GetCrucialDate(Paycheck paycheck, Dependent dependent);

    /// <summary>
    /// Gets the name of the item visible in the paycheck for the dependent.
    /// </summary>
    protected virtual string GetName(Paycheck paycheck, Dependent dependent) => GetType().Name;

    /// <summary>
    /// Calculates the daily rate for the dependent.
    /// </summary>
    protected abstract decimal GetDailyRate(Paycheck paycheck);
}
