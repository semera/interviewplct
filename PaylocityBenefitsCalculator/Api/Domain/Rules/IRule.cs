using Api.Domain.Entities;

namespace Api.Domain.Rules;

/// <summary>
/// Represents a rule that applies to a paycheck for an employee.
/// </summary>
public interface IRule
{
    /// <summary>
    /// Applies the rule to the specified paycheck and employee.
    /// </summary>
    /// <param name="paycheck">The paycheck to apply the rule to.</param>
    /// <param name="employee">The employee to apply the rule for.</param>
    void Apply(Paycheck paycheck, Employee employee);
}
