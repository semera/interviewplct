namespace Api.Domain.Configs;

/// <summary>
/// Static configuration values.
/// </summary>
public class Config : IConfig
{
    public int DaysPerYear => 365;

    public decimal BaseCostPerMonth => 1_000m;

    public decimal HighSalaryCostCoef => 0.02m;

    public decimal HighSalaryCostLimit => 80_000m;

    public decimal FiftyYearsCostPerMonth => 200m;

    public decimal DependentCostByMonth => 600m;
}
