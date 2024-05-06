using System;

namespace Api.Domain.Tools;

/// <summary>
/// Interface for calculating the number of days between two dates.
/// </summary>
public interface IDaysCalculator
{
    /// <summary>
    /// Calculates the number of days between the start date and end date, inclusive.
    /// </summary>
    /// <param name="startDate">The start date.</param>
    /// <param name="endDate">The end date.</param>
    /// <returns>The number of days between the start date and end date, inclusive.</returns>
    int Days(DateTime startDate, DateTime endDate);
}
