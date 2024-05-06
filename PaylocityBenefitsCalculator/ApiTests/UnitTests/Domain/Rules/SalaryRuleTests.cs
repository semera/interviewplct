using System;
using Api.Domain.Entities;
using Api.Domain.Rules;
using Api.Domain.Tools;
using Moq;
using Xunit;

namespace ApiTests.Rules;

public class SalaryRuleTests
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

        int salary = 3650;

        Paycheck paycheck = new() { AnnualSalary = salary, Period = payPeriod };

        Mock<IDaysCalculator> daysCalculatorMock = new();
        daysCalculatorMock.Setup(x => x.Days(payPeriod.StartDate, payPeriod.EndDate)).Returns(14);

        Mock<IRatesCalculator> ratesCalculatorMock = new();
        ratesCalculatorMock.Setup(x => x.DailyRateFromAnual(salary)).Returns(10);


        SalaryRule rule = new(daysCalculatorMock.Object, ratesCalculatorMock.Object);

        Employee employee = new EmployeeBuilder().Build();

        // Act
        rule.Apply(paycheck, employee);

        // Assert
        Assert.Equal(140, paycheck.NetSalary); // 3650 / 365 * 14
        Assert.Single(paycheck.Items);
        Assert.Equal(10, paycheck.Items[0].DailyRate);
        Assert.Equal(14, paycheck.Items[0].Days);
        Assert.Equal("base salary", paycheck.Items[0].Name);
    }
}
