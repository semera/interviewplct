using Api.Domain.Entities;

namespace Api.Dtos.Paycheck;

/// <summary>
/// Represents the DTO for a paycheck.
/// </summary>
public class PaycheckDto
{
    /// <summary>
    /// Gets or sets the annual salary.
    /// </summary>
    public required decimal AnnualSalary { get; set; }

    /// <summary>
    /// Gets or sets the pay period.
    /// </summary>
    public required PayPeriod Period { get; set; }

    /// <summary>
    /// Gets or sets the net salary.
    /// </summary>
    public required decimal NetSalary { get; set; }

    /// <summary>
    /// Gets or sets the list of paycheck item DTOs.
    /// </summary>
    public required List<PaycheckItemDto> Items { get; set; }
}
