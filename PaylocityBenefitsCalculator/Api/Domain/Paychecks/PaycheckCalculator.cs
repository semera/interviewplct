using System.Collections.Generic;
using Api.Domain.Entities;
using Api.Domain.Rules;

namespace Api.Domain.Paychecks;

/// <summary>
/// Calculates the paycheck for the given pay period and employee.
/// </summary>
/// <param name="payPeriod">The pay period for which the paycheck is calculated.</param>
/// <param name="employee">The employee for whom the paycheck is calculated.</param>
/// <returns>The calculated paycheck.</returns>
public class PaycheckCalculator(IEnumerable<IRule> rules) : IPaycheckCalculator
{
    /// <inheritdoc/>
    public Paycheck Calculate(PayPeriod payPeriod, Employee employe)
    {
        Paycheck paycheck = new() { AnnualSalary = employe.Salary, Period = payPeriod };

        foreach (IRule rule in rules)
        {
            rule.Apply(paycheck, employe);
        }

        return paycheck;
    }
}
