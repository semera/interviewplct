using System;

namespace Api.Domain.Tools;

/// <inheritdoc/>
public class CalendarTools : ICalendarTools
{
    /// <inheritdoc/>
    public DateTime FirstMondayOfYear(int year)
    {
        DateTime newYear = new(year, 1, 1);
        return new(year, 1, (8 - (int)newYear.DayOfWeek) % 7 + 1);
    }

    public bool IsCrucialDate(DateTime testedDate, ref DateTime startDate, DateTime endDate)
    {
        if (testedDate <= startDate)
        {
            return true;
        }
        if (testedDate <= endDate)
        {
            startDate = testedDate;
            return true;
        }
        return false;
    }
}
