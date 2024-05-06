using System;

namespace Api.Domain.Tools;

/// <summary>
/// Provides tools for working with calendars.
/// </summary>
public interface ICalendarTools
{
    /// <summary>
    /// Gets the date of the first Monday of the specified year.
    /// </summary>
    /// <param name="year">The year.</param>
    /// <returns>The date of the first Monday of the year.</returns>
    DateTime FirstMondayOfYear(int year);

    /// <summary>
    /// Determines if the tested date is a crucial date for payment calculation.
    /// This means that testedDate is before (inclusive) endDate. 
    /// If testedDate is between startDate and endDate, then true is returned startDate is set to testedDate. 
    /// If testedDate is later than endDate, false is returned and startDate remains unchanged. 
    /// If testedDate is earlier than startDate, true is returned and startDate remains unchanged.
    /// </summary>
    /// <param name="testedDate">The tested date.</param>
    /// <param name="startDate">The start date.</param>
    /// <param name="endDate">The end date.</param>
    /// <returns>True if the tested date is a crucial date; otherwise, false.</returns>
    bool IsCrucialDate(DateTime testedDate, ref DateTime startDate, DateTime endDate);
}
