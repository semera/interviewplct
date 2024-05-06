using System;
using Api.Domain.Configs;
using Api.Domain.Entities;
using Api.Domain.Rules;
using Api.Domain.Tools;
using Moq;
using Xunit;

namespace ApiTests.Rules;

public class BaseCostRuleTests
{
    [Fact]
    public void Apply_PaycheckUpdated()
    {
        // Arrange
        PayPeriod payPeriod = new()
        {
            StartDate = new DateTime(2022, 1, 1),
            EndDate = new DateTime(2022, 1, 14)
        };

        int salary = int.MinValue; // don't care
        Paycheck paycheck = new() { AnnualSalary = salary, Period = payPeriod };

        Config config = new();

        Mock<IDaysCalculator> daysCalculatorMock = new();
        daysCalculatorMock.Setup(x => x.Days(payPeriod.StartDate, payPeriod.EndDate)).Returns(14);

        Mock<IRatesCalculator> ratesCalculatorMock = new();
        ratesCalculatorMock.Setup(x => x.DailyRateFromMonth(config.BaseCostPerMonth)).Returns(32.88m); // from 1000 * 12 /365 should be 32.88m 

        BaseCostRule rule = new(daysCalculatorMock.Object, ratesCalculatorMock.Object, config);

        Employee employee = new EmployeeBuilder().Build();

        // Act
        rule.Apply(paycheck, employee);

        // Assert
        Assert.Equal(-460.32m, paycheck.NetSalary); // 32.88m * 14
        Assert.Single(paycheck.Items);
        Assert.Equal(-32.88m, paycheck.Items[0].DailyRate);
        Assert.Equal(14, paycheck.Items[0].Days);
        Assert.Equal("base cost", paycheck.Items[0].Name);
    }
}
