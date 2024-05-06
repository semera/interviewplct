using Api.Domain.Entities;

namespace Api.Domain.Paychecks;

/// <summary>
/// Interface for calculating the paycheck.
/// </summary>
public interface IPaycheckCalculator
{
    /// <summary>
    /// Calculates the paycheck for the given pay period and employee.
    /// </summary>
    /// <param name="payPeriod">The pay period for which the paycheck is calculated.</param>
    /// <param name="employee">The employee for whom the paycheck is calculated.</param>
    /// <returns>The calculated paycheck.</returns>
    Paycheck Calculate(PayPeriod payPeriod, Employee employee);
}
