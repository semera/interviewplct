using System;
using Api.Domain.Configs;
using Api.Domain.Entities;
using Api.Domain.Rules;
using Api.Domain.Tools;
using Xunit;

namespace ApiTests.Rules;

public class FiftyYearsDependentRuleTests
{
    [Theory]
    [InlineData(1, 14, -92.12)]  // 50 years old day before period    - 14 days counted
    [InlineData(2, 14, -92.12)]  // 50 years first day of period    - 14 days counted
    [InlineData(15, 1, -6.58)]  // 50 years last day of period       - 1 day counted
    public void Apply_Exactly50DuringPeriod_PaycheckUpdated(int day, int daysCalculated, decimal netSalary)
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
        FiftyYearsDependentRule rule = new(new DaysCalculator(), new CalendarTools(), new RatesCalculator(config), config);

        Employee employee = new EmployeeBuilder()
            .WithDependent(birthday: new DateTime(2022, 2, day).AddYears(-50))
            .Build();

        // Act
        rule.Apply(paycheck, employee);

        // Assert
        Assert.Equal(netSalary, paycheck.NetSalary);
        Assert.Single(paycheck.Items);
        Assert.Equal(-6.58m, paycheck.Items[0].DailyRate);
        Assert.Equal(daysCalculated, paycheck.Items[0].Days);
        Assert.StartsWith("50 year dependent ", paycheck.Items[0].Name);
    }

    [Fact]
    public void Apply_Exactly50AfterPeriod_PaycheckNotChanged()
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
        FiftyYearsDependentRule rule = new(new DaysCalculator(), new CalendarTools(), new RatesCalculator(config), config);

        Employee employee = new EmployeeBuilder()
            .WithDependent(birthday: new DateTime(2022, 2, 16).AddYears(-50))
            .Build();

        // Act
        rule.Apply(paycheck, employee);

        // Assert
        Assert.Equal(0, paycheck.NetSalary);
        Assert.Empty(paycheck.Items);
    }


    [Fact]
    public void Apply_TwoOldChildrenAndOneYoung_PaycheckUpdated()
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
        FiftyYearsDependentRule rule = new(new DaysCalculator(), new CalendarTools(), new RatesCalculator(config), config);

        Employee employee = new EmployeeBuilder()
            .WithDependent(birthday: new DateTime(2022, 2, 2).AddYears(-20)) // young child, will not in the paycheck
            .WithDependent(birthday: new DateTime(2022, 2, 2).AddYears(-50)) // 50 years first day of period    - 14 days counted
            .WithDependent(birthday: new DateTime(2022, 2, 15).AddYears(-50)) // 50 years last day of period       - 1 day counted
            .Build();

        // Act
        rule.Apply(paycheck, employee);

        // Assert
        Assert.Equal(-92.12m + -6.58m, paycheck.NetSalary);
        Assert.Equal(2, paycheck.Items.Count);

        Assert.Equal(-6.58m, paycheck.Items[0].DailyRate);
        Assert.Equal(14, paycheck.Items[0].Days);
        Assert.StartsWith("50 year dependent ", paycheck.Items[0].Name);

        Assert.Equal(-6.58m, paycheck.Items[1].DailyRate);
        Assert.Equal(1, paycheck.Items[1].Days);
        Assert.StartsWith("50 year dependent ", paycheck.Items[1].Name);
    }
}
