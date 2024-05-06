using System;
using Api.Domain.Tools;
using Xunit;

namespace Api.Domain.PaycheckCalculator.Tests;

public class CalendarToolsTests
{
    [Theory]
    [InlineData(2023, 2)]
    [InlineData(2024, 1)]
    [InlineData(2025, 6)]
    public void FirstMondayOfYear_IsCorrectlyCalculated(int year, int expectedDay)
    {
        // Arrange
        CalendarTools tools = new();

        // Act
        DateTime firstMonday = tools.FirstMondayOfYear(year);

        // Assert
        Assert.Equal(expectedDay, firstMonday.Day);
    }
}
