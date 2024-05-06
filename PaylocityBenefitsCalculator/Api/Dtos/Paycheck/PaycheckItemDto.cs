namespace Api.Dtos.Paycheck;

/// <summary>
/// Represents the DTO for a paycheck item.
/// </summary>
public class PaycheckItemDto
{
    /// <summary>
    /// Gets or sets the name of the paycheck item.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the daily rate, rounded to 2 decimal places.
    /// </summary>
    public required decimal DailyRate { get; set; }

    /// <summary>
    /// Gets or sets the number of days the daily rate was counted.
    /// </summary>
    public required int Days { get; set; }

    /// <summary>
    /// Gets or sets the value added or removed from the net salary.
    /// </summary>
    public required decimal Salary { get; set; }
}
