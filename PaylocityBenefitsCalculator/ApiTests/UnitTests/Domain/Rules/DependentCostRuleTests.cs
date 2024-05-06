using System;
using Api.Domain.Configs;
using Api.Domain.Entities;
using Api.Domain.Rules;
using Api.Domain.Tools;
using Xunit;

namespace ApiTests.Rules;

public class DependentCostRuleTests
{
    [Theory]
    [InlineData(1, 14, -276.22)]  // born before period    - 14 days counted
    [InlineData(2, 14, -276.22)]  // born first day of period    - 14 days counted
    [InlineData(15, 1, -19.73)]  // born last day of period       - 1 day counted
    public void Apply_ChildBornDuringPeriod_PaycheckUpdated(int day, int daysCalculated, decimal netSalary)
    {
        // Arrange
        PayPeriod payPeriod = new()
        {
            StartDate = new DateTime(2022, 2, 2),
            EndDate = new DateTime(2022, 2, 15)
        };

        int salary = 100_000;
        Paycheck paycheck = new() { AnnualSalary = salary, Period = payPeriod };

        Config config = new();

        // TODO: add mocks, now it's ok because config is hardcoded for now
        DependentCostRule rule = new(new DaysCalculator(), new CalendarTools(), new RatesCalculator(config), config);

        Employee employee = new EmployeeBuilder()
            .WithDependent(birthday: new DateTime(2022, 2, day))
            .Build();

        // Act
        rule.Apply(paycheck, employee);

        // Assert
        Assert.Equal(netSalary, paycheck.NetSalary);
        Assert.Single(paycheck.Items);
        Assert.Equal(-19.73m, paycheck.Items[0].DailyRate);
        Assert.Equal(daysCalculated, paycheck.Items[0].Days);
        Assert.StartsWith("dependent cost", paycheck.Items[0].Name);
    }

    [Fact]
    public void Apply_BornAfterPeriod_PaycheckNotChanged()
    {
        // Arrange
        PayPeriod payPeriod = new()
        {
            StartDate = new DateTime(2022, 2, 2),
            EndDate = new DateTime(2022, 2, 15)
        };

        int salary = 100_000;
        Paycheck paycheck = new() { AnnualSalary = salary, Period = payPeriod };

        Config config = new();

        // TODO: add mocks, now it's ok because config is hardcoded for now
        DependentCostRule rule = new(new DaysCalculator(), new CalendarTools(), new RatesCalculator(config), config);

        Employee employee = new EmployeeBuilder()
            .WithDependent(birthday: new DateTime(2022, 2, 16))
            .Build();

        // Act
        rule.Apply(paycheck, employee);

        // Assert
        Assert.Equal(0, paycheck.NetSalary);
        Assert.Empty(paycheck.Items);
    }
}
