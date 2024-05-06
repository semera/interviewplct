using System;

namespace Api.Domain.Tools;

public static class RoundExtensions
{
    /// <summary>
    /// Extension method to round a decimal value to two decimal places.
    /// </summary>
    /// <param name="value">The decimal value to round.</param>
    /// <returns>The rounded decimal value.</returns>
    public static decimal Round(this decimal value)
    {
        // TODO: I would consider manually rounding to floor or ceil based on the scenario. It would be always disadvantageous for the employer.
        return Math.Round(value, 2);
    }
}
