using Api.Domain.Entities;

namespace Api.Domain.Validations;

public class EmployeeCanHaveOnlyOnePartnerValidation : IValidation
{
    /// <summary>
    /// Validates if the employee can have only one partner.
    /// </summary>
    public void IsValidData(Employee employee, int year)
    {
        // TODO: handle none?
        if (employee.Dependents.Count(dependent => dependent.Relationship != Relationship.Child) > 1)
        {
            throw new InvalidOperationException("Employee can have only one partner");
        }
    }
}
