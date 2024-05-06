using System;
using Api.Domain.Configs;
using Api.Domain.Entities;
using Api.Domain.Tools;

namespace Api.Domain.Rules;

/// <summary>
/// dependents that are over 50 years old will incur an additional $200 per month
/// </summary>
public class FiftyYearsDependentRule(IDaysCalculator daysCalculator, ICalendarTools calendarTools, IRatesCalculator ratesCalculator, IConfig config)
    : DependentRuleBase(daysCalculator, calendarTools)
{
    protected override DateTime GetCrucialDate(Paycheck paycheck, Dependent dependend)
        => dependend.DateOfBirth.AddYears(50);

    protected override decimal GetDailyRate(Paycheck paycheck)
        => ratesCalculator.DailyRateFromMonth(config.FiftyYearsCostPerMonth) * Consts.Negative;

    protected override string GetName(Paycheck paycheck, Dependent dependent)
        => $"50 year dependent {dependent.LastName}, {dependent.FirstName}";
}
