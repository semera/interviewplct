using Api.Domain.Entities;

namespace Api.Domain.Paychecks;

/// <summary>
/// Represents a service for calculating paychecks.
/// </summary>
public interface IPaycheckService
{
    /// <summary>
    /// Gets the paychecks for the specified employee and year.
    /// </summary>
    /// <param name="employee">The employee for whom the paychecks are calculated.</param>
    /// <param name="year">The year for which the paychecks are calculated.</param>
    /// <returns>An array of paychecks.</returns>
    Paycheck[] GetPaychecks(Employee employee, int year);
}
