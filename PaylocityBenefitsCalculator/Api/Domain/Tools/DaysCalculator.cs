using System;

namespace Api.Domain.Tools;

/// <inheritdoc />
public class DaysCalculator : IDaysCalculator
{
    /// <inheritdoc />
    public int Days(DateTime startDate, DateTime endDate)
    {
        return (endDate - startDate).Days + 1;
    }
}
