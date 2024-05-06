using Api.Domain.Entities;
using Api.Domain.Segmenting;
using Api.Domain.Validations;

namespace Api.Domain.Paychecks;

/// <inheritdoc/>
public class PaycheckService(IPaycheckCalculator paycheckCalculator, IPayPeriodSegmenter payPeriodSegmenter, IEnumerable<IValidation> validations) : IPaycheckService
{
    /// <inheritdoc/>
    public Paycheck[] GetPaychecks(Employee employe, int year)
    {
        // TODO: this would be better to handle inside of a validation provider, consider warning and error messages from validations
        // this is incosistend data, not real validation, writes 500 internal server error
        foreach (IValidation validation in validations)
        {
            validation.IsValidData(employe, year);
        }

        return payPeriodSegmenter
            .GetPayPeriods(year)
            .Select(payPeriod => paycheckCalculator.Calculate(payPeriod, employe))
            .ToArray();
    }
}
