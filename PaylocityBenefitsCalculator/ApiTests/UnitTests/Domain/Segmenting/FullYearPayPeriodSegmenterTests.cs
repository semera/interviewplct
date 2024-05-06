using System;
using Api.Domain.Configs;
using Api.Domain.Entities;
using Api.Domain.Segmenting;
using Xunit;

namespace ApiTests.Segmenting;

public class FullYearPayPeriodSegmenterTests
{
    [Fact]
    public void GetPayPeriods_LeapYear_ReturnsCorrectPeriods()
    {
        // Arrange
        int year = 2024;
        FullYearPayPeriodSegmenter segmenter = new();

        // Act
        PayPeriod[] payPeriods = [.. segmenter.GetPayPeriods(year)];

        // Assert
        Assert.Equal(Consts.PayPeriodsPerYear, payPeriods.Length);
        Assert.Equal(new DateTime(year, 1, 1), payPeriods[0].StartDate);
        Assert.Equal(new DateTime(year, 1, 15), payPeriods[0].EndDate);

        Assert.Equal(new DateTime(year, 1, 16), payPeriods[1].StartDate);
        Assert.Equal(new DateTime(year, 1, 30), payPeriods[1].EndDate);

        Assert.Equal(new DateTime(year, 12, 18), payPeriods[25].StartDate);
        Assert.Equal(new DateTime(year, 12, 31), payPeriods[25].EndDate);
    }

    [Fact]
    public void GetPayPeriods_NonLeapYear_ReturnsCorrectPeriods()
    {
        // Arrange
        int year = 2022;
        FullYearPayPeriodSegmenter segmenter = new();

        // Act
        PayPeriod[] payPeriods = [.. segmenter.GetPayPeriods(year)];

        // Assert
        Assert.Equal(Consts.PayPeriodsPerYear, payPeriods.Length);
        Assert.Equal(new DateTime(year, 1, 1), payPeriods[0].StartDate);
        Assert.Equal(new DateTime(year, 1, 15), payPeriods[0].EndDate);

        Assert.Equal(new DateTime(year, 1, 16), payPeriods[1].StartDate);
        Assert.Equal(new DateTime(year, 1, 29), payPeriods[1].EndDate);

        Assert.Equal(new DateTime(year, 12, 18), payPeriods[25].StartDate);
        Assert.Equal(new DateTime(year, 12, 31), payPeriods[25].EndDate);
    }
}
