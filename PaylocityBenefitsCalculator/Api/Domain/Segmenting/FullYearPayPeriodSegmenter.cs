using System;
using System.Collections.Generic;
using Api.Domain.Configs;
using Api.Domain.Entities;

namespace Api.Domain.Segmenting;

/// <summary>
/// The logic divides the year into 14-day segments starting from New Year's Day.
/// To account for all 365 days, any remaining day is added to the first segment.
/// In the case of a leap year, an additional day is included in the second segment.
/// Therefore, the first and second segments may each contain 15 days.
/// </summary>
public class FullYearPayPeriodSegmenter : IPayPeriodSegmenter
{
    /// <inheritdoc />
    public IEnumerable<PayPeriod> GetPayPeriods(int year)
    {
        bool isLeapYear = DateTime.IsLeapYear(year);
        int segmentLength = Consts.PayPeriodDays;

        DateTime startDate = new(year, 1, 1);

        for (int i = 0; i < Consts.PayPeriodsPerYear; i++)
        {
            DateTime endDate = startDate.AddDays(segmentLength - 1);

            // first (and second when leap year) segments may contain 15 days
            if (i == 0 || i == 1 && isLeapYear)
            {
                endDate = endDate.AddDays(1);
            }

            yield return new PayPeriod { StartDate = startDate, EndDate = endDate };

            startDate = endDate.AddDays(1);
        }
    }
}
