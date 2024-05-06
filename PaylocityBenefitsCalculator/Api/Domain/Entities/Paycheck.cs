using System.Collections.Generic;

namespace Api.Domain.Entities;

/// <summary>
/// Represents a paycheck.
/// </summary>
public record Paycheck
{
    // TODO: salary could be used from employee

    /// <summary>
    /// Gets or sets the annual salary.
    /// </summary>
    public required decimal AnnualSalary { get; init; }

    /// <summary>
    /// Gets or sets the pay period.
    /// </summary>
    public required PayPeriod Period { get; init; }

    /// <summary>
    /// Gets or sets the net salary.
    /// </summary>
    public decimal NetSalary { get; set; }

    /// <summary>
    /// Gets or sets the list of paycheck items.
    /// </summary>
    public List<PaycheckItem> Items { get; private set; } = [];
}
