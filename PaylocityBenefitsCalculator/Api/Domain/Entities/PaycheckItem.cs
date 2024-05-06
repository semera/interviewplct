namespace Api.Domain.Entities;

/// <summary>
/// Represents an item in a paycheck.
/// </summary>
public record PaycheckItem
{
    // TODO: Days, DailyRate could be changed to more generic properties like item, value and add item type property

    /// <summary>
    /// Information about what the numbers mean.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Value per day, rounded to 2 decimal places.
    /// </summary>
    public required decimal DailyRate { get; init; }

    /// <summary>
    /// Number of times the DailyRate was counted.
    /// </summary>
    public required int Days { get; init; }

    /// <summary>
    /// Value added or removed from the NetSalary.
    /// </summary>
    public decimal Salary => DailyRate * Days;
}
