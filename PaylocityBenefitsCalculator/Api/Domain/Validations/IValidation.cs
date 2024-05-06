using Api.Domain.Entities;

namespace Api.Domain.Validations;

/// <summary>
/// Represents the interface for employee data validation.
/// </summary>
public interface IValidation
{
    /// <summary>
    /// Validates the employee data.
    /// </summary>
    public void IsValidData(Employee employee, int year);
}
