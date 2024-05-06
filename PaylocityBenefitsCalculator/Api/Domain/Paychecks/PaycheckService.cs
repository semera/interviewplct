using Api.Domain.Entities;
using Api.Domain.Segmenting;

namespace Api.Domain.Paychecks;

/// <inheritdoc/>
public class PaycheckService(IPaycheckCalculator paycheckCalculator, IPayPeriodSegmenter payPeriodSegmenter) : IPaycheckService
{
    /// <inheritdoc/>
    public Paycheck[] GetPaychecks(Employee employe, int year)
    {
        // TODO: add input data (employee only for now) validation

        return payPeriodSegmenter
            .GetPayPeriods(year)
            .Select(payPeriod => paycheckCalculator.Calculate(payPeriod, employe))
            .ToArray();
    }
}
