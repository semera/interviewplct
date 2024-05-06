using System;
using Api.Domain.Configs;
using Api.Domain.Entities;
using Api.Domain.Tools;

namespace Api.Domain.Rules;

/// <summary>
/// each dependent represents an additional $600 cost per month (for benefits)
/// </summary>
public class DependentCostRule(IDaysCalculator daysCalculator, ICalendarTools calendarTools, IRatesCalculator ratesCalculator, IConfig config)
    : DependentRuleBase(daysCalculator, calendarTools)
{
    /// <inheritdoc/>
    protected override DateTime GetCrucialDate(Paycheck paycheck, Dependent dependend)
        => dependend.DateOfBirth;

    /// <inheritdoc/>
    protected override decimal GetDailyRate(Paycheck paycheck)
        => ratesCalculator.DailyRateFromMonth(config.DependentCostByMonth) * Consts.Negative;

    /// <inheritdoc/>
    protected override string GetName(Paycheck paycheck, Dependent dependent)
        => $"dependent cost {dependent.LastName}, {dependent.FirstName}";
}
