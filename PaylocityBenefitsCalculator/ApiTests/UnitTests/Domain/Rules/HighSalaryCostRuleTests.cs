using System;
using Api.Domain.Configs;
using Api.Domain.Entities;
using Api.Domain.Rules;
using Api.Domain.Tools;
using Moq;
using Xunit;

namespace ApiTests.Rules;

public class HighSalaryCostRuleTests
{
    [Fact]
    public void Apply_SalaryOver80K_PaycheckUpdated()
    {
        // Arrange
        PayPeriod payPeriod = new()
        {
            StartDate = new DateTime(2022, 1, 1),
            EndDate = new DateTime(2022, 1, 14)
        };

        int salary = 100_000;
        Paycheck paycheck = new() { AnnualSalary = salary, Period = payPeriod };

        Config config = new();

        Mock<IDaysCalculator> daysCalculatorMock = new();
        daysCalculatorMock.Setup(x => x.Days(payPeriod.StartDate, payPeriod.EndDate)).Returns(14);

        Mock<IRatesCalculator> ratesCalculatorMock = new();
        ratesCalculatorMock.Setup(x => x.DailyRateFromAnual(2000)).Returns(5.5m); // 200= 2% from 10000 result = 200 / 365


        HighSalaryCostRule rule = new(daysCalculatorMock.Object, ratesCalculatorMock.Object, config);


        Employee employee = new EmployeeBuilder().Build();

        // Act
        rule.Apply(paycheck, employee);

        // Assert
        Assert.Equal(-77m, paycheck.NetSalary); // 0.55m * 14
        Assert.Single(paycheck.Items);
        Assert.Equal(-5.5m, paycheck.Items[0].DailyRate);
        Assert.Equal(14, paycheck.Items[0].Days);
        Assert.Equal("high salary cost", paycheck.Items[0].Name);

    }

    [Fact]
    public void Apply_SalaryExactly80K_PaycheckNotUpdated()
    {
        // Arrange
        PayPeriod payPeriod = new()
        {
            StartDate = new DateTime(2022, 1, 1),
            EndDate = new DateTime(2022, 1, 14)
        };

        int salary = 80_000;
        Paycheck paycheck = new() { AnnualSalary = salary, Period = payPeriod };

        Config config = new();

        Mock<IDaysCalculator> daysCalculatorMock = new();
        Mock<IRatesCalculator> ratesCalculatorMock = new();

        HighSalaryCostRule rule = new(daysCalculatorMock.Object, ratesCalculatorMock.Object, config);

        Employee employee = new EmployeeBuilder().Build();

        // Act
        rule.Apply(paycheck, employee);

        // Assert
        Assert.Equal(0, paycheck.NetSalary);
        Assert.Empty(paycheck.Items);
    }

}
