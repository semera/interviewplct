using System;
using System.Collections.Generic;
using Api.Domain.Configs;
using Api.Domain.Entities;
using Api.Domain.Tools;

namespace Api.Domain.Segmenting;

/// <summary>
/// Tha logic divides the year into 26 of 14-day segments starting from the first Monday of the year.
/// It means the last can overlap into the next year.
/// </summary>
/// <param name="calendarTools">The calendar tools used to calculate the pay periods.</param>
public class TwoWeeksPayPeriodSegmenter(ICalendarTools calendarTools) : IPayPeriodSegmenter
{
    /// <inheritdoc />
    public IEnumerable<PayPeriod> GetPayPeriods(int year)
    {
        DateTime monday = calendarTools.FirstMondayOfYear(year);

        // TODO: in case of start on 1.1, or 2.1. during leap year could be nice as 27th segment 
        for (int i = 0; i < Consts.PayPeriodsPerYear; i++)
        {
            yield return new PayPeriod
            {
                StartDate = monday.AddDays(i * 14),
                EndDate = monday.AddDays(i * 14 + 13)
            };
        }
    }
}
