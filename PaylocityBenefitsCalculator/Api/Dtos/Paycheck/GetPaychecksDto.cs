namespace Api.Dtos.Paycheck;

/// <summary>
/// Represents the DTO for a list of paychecks.
/// </summary>
public class GetPaychecksDto
{
    /// <summary>
    /// Gets or sets the list of paycheck DTOs.
    /// </summary>
    public required List<PaycheckDto> Paychecks { get; set; }
}
