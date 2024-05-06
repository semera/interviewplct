namespace Api.Domain.Tools;

/// <summary>
/// Interface for calculating the daily rate from a value.
/// </summary>
public interface IRatesCalculator
{
    /// <summary>
    /// Calculates the daily rate based on the provided annual value.
    /// </summary>
    /// <param name="annualValue">The annual value.</param>
    /// <returns>The calculated daily rate.</returns>
    decimal DailyRateFromAnual(decimal annualValue);

    /// <summary>
    /// Calculates the daily rate based on the provided month value.
    /// </summary>
    /// <param name="monthValue">The month value.</param>
    /// <returns>The calculated daily rate.</returns>
    decimal DailyRateFromMonth(decimal monthValue);
}
