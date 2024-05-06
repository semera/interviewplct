using System;
using Api.Domain.Tools;
using Xunit;

namespace Api.Domain.PaycheckCalculator.Tests;

public class DaysCalculatorTests
{
    [Theory]
    [InlineData("2023-01-05", "2023-01-05", 1)]
    [InlineData("2023-01-05", "2023-01-06", 2)]
    public void DaysTest(DateTime startDate, DateTime endDate, int expectedDays)
    {
        // Arrange
        DaysCalculator calculator = new();

        // Act
        int days = calculator.Days(startDate, endDate);

        // Assert
        Assert.Equal(expectedDays, days);
    }
}
