namespace Api.Domain.Configs;

// TODO: I would condider this cannot be production approach (increased salary during year
// for example), so doesn't make sence move configuration to appsettings.json, database and so on.

/// <summary>
/// Values expected to be configurable.
/// </summary>
public interface IConfig
{
    /// <summary>
    /// TODO: days per year, always consider 365 days. Requirement is good to discuss.
    /// </summary>
    int DaysPerYear { get; }

    /// <summary>
    /// employees have a base cost of $1,000 per month (for benefits)
    /// </summary>
    decimal BaseCostPerMonth { get; }

    /// <summary>
    /// employees that make more than $80,000 per year will incur an - tional 2% of their yearly salary in benefits costs
    /// </summary>
    decimal HighSalaryCostCoef { get; }

    /// <summary>
    /// employees that make more than $80,000 per year will incur an - tional 2% of their yearly salary in benefits costs
    /// </summary>
    decimal HighSalaryCostLimit { get; }

    /// <summary>
    /// employees with dependents have a $200 discount on their benefits
    /// </summary>
    decimal FiftyYearsCostPerMonth { get; }

    /// <summary>
    /// each dependent represents an additional $600 cost per month (for - fits)
    /// </summary>
    decimal DependentCostByMonth { get; }
}
