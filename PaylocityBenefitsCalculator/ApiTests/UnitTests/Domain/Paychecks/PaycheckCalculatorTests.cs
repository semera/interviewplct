using System;
using System.Collections.Generic;
using Api.Domain.Configs;
using Api.Domain.Entities;
using Api.Domain.Rules;
using Api.Domain.Tools;
using ApiTests;
using Moq;
using Xunit;

namespace Api.Domain.Paychecks.Tests;

public class PaycheckCalculatorTests
{
    [Fact]
    public void Calculate_TwoRules_ShouldApplyAllRulesAndReturnPaycheckWithAnualSalary()
    {
        // Arrange
        PayPeriod payPeriod = new()
        {
            StartDate = new DateTime(2022, 1, 1),
            EndDate = new DateTime(2022, 1, 14)
        };

        Employee employee = new EmployeeBuilder().WithSalary(50_000).Build();

        Mock<IRule> salaryRuleMock = new();
        Mock<IRule> dependentRuleMock = new();

        List<IRule> rules =
        [
            salaryRuleMock.Object,
            dependentRuleMock.Object
        ];

        PaycheckCalculator paycheckCalculator = new(rules);

        // Act
        Paycheck paycheck = paycheckCalculator.Calculate(payPeriod, employee);

        // Assert
        Assert.Equal(50000, paycheck.AnnualSalary);
        Assert.Equal(payPeriod, paycheck.Period);
        Assert.Equal(0, paycheck.NetSalary); // Zero because rules are mocked
        Assert.Empty(paycheck.Items); // No items added because rules are mocked

        salaryRuleMock.Verify(rule => rule.Apply(paycheck, employee), Times.Once);
        dependentRuleMock.Verify(rule => rule.Apply(paycheck, employee), Times.Once);
    }


    [Fact]
    public void Calculate_AllRulesPrivateTest_JustForExperiments()
    {
        // TODO: just for experiments, not official test

        // Arrange
        PayPeriod payPeriod = new()
        {
            StartDate = new DateTime(2022, 1, 1),
            EndDate = new DateTime(2022, 1, 30)
        };

        Employee employee = new EmployeeBuilder()
            .WithSalary(100_000)
            .WithDependent(birthday: new DateTime(1922, 1, 15)) // 50years
            .Build();

        Config config = new();
        CalendarTools calendarTools = new();
        RatesCalculator ratesCalculator = new(config);
        DaysCalculator daysCalculator = new();

        List<IRule> rules =
        [
            new SalaryRule(daysCalculator, ratesCalculator),
            new BaseCostRule(daysCalculator, ratesCalculator, config),
            new HighSalaryCostRule(daysCalculator, ratesCalculator, config),
            new DependentCostRule(daysCalculator, calendarTools, ratesCalculator, config),
            new FiftyYearsDependentRule(daysCalculator, calendarTools, ratesCalculator, config),
        ];

        PaycheckCalculator paycheckCalculator = new(rules);

        // Act
        Paycheck paycheck = paycheckCalculator.Calculate(payPeriod, employee);

        // Assert
        //Assert.Equal("fail to get output, comment to pass", paycheck.DumpString());
    }
}
