using Api.Domain.Entities;

namespace Api.Services;

/// <summary>
/// Represents a data access object for employees.
/// </summary>
public interface IEmployeesDao
{
    /// <summary>
    /// Retrieves an employee by their ID.
    /// </summary>
    /// <param name="id">The ID of the employee.</param>
    /// <returns>The employee with the specified ID, or null if not found.</returns>
    Task<Employee?> GetEmployee(int id);

    /// <summary>
    /// Retrieves all employees.
    /// </summary>
    /// <returns>A list of all employees.</returns>
    Task<List<Employee>> GetAllEmployees();

    /// <summary>
    /// Retrieves a dependent by their ID.
    /// </summary>
    /// <param name="id">The ID of the dependent.</param>
    /// <returns>The dependent with the specified ID, or null if not found.</returns>
    Task<Dependent?> GetDependent(int id);

    /// <summary>
    /// Retrieves all dependents.
    /// </summary>
    /// <returns>A list of all dependents.</returns>
    Task<List<Dependent>> GetAllDependent();
}
