using System.Collections.Generic;
using Api.Domain.Entities;

namespace Api.Domain.Segmenting;

/// <summary>
/// Interface for dividing the year into pay periods.
/// </summary>
public interface IPayPeriodSegmenter
{
    /// <summary>
    /// Gets the pay periods for the specified year.
    /// </summary>
    /// <param name="year">The year for which to get the pay periods.</param>
    /// <returns>An enumerable collection of pay periods.</returns>
    IEnumerable<PayPeriod> GetPayPeriods(int year);
}
