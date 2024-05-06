using System;

namespace Api.Domain.Entities;

/// <summary>
/// Represents respective start and end dates for paycheck.
/// </summary>
public readonly record struct PayPeriod
{
    /// <summary>
    /// Gets the start date of the pay period. Inclusive.
    /// </summary>
    public required DateTime StartDate { get; init; }

    /// <summary>
    /// Gets the end date of the pay period. Inclusive.
    /// </summary>
    public required DateTime EndDate { get; init; }
}
