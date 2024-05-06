using System;
using Api.Domain.Configs;
using Api.Domain.Entities;
using Api.Domain.Tools;
using Moq;
using Xunit;

namespace Api.Domain.Segmenting.Tests;

public class TwoWeeksPayPeriodSegmenterTests
{
    [Fact]
    public void GetPayPeriods_NewYearIsMonday_LastPeriodEndsDecember30()
    {
        // Arrange
        int year = 2023;

        Mock<ICalendarTools> calendarToolsMock = new();
        calendarToolsMock.Setup(x => x.FirstMondayOfYear(year)).Returns(new DateTime(year, 1, 1));

        TwoWeeksPayPeriodSegmenter segmenter = new(calendarToolsMock.Object);

        // Act
        PayPeriod[] payPeriods = [.. segmenter.GetPayPeriods(year)];

        // Assert
        Assert.Equal(Consts.PayPeriodsPerYear, payPeriods.Length);

        Assert.Equal(new DateTime(year, 1, 1), payPeriods[0].StartDate);
        Assert.Equal(new DateTime(year, 1, 14), payPeriods[0].EndDate);

        Assert.Equal(new DateTime(year, 12, 17), payPeriods[25].StartDate);
        Assert.Equal(new DateTime(year, 12, 30), payPeriods[25].EndDate);
    }

    [Fact]
    public void GetPayPeriods_NewYearIsSunday_LastPeriodEndsDecember31()
    {
        // Arrange
        int year = 2023;

        Mock<ICalendarTools> calendarToolsMock = new();
        calendarToolsMock.Setup(x => x.FirstMondayOfYear(year)).Returns(new DateTime(year, 1, 2));

        TwoWeeksPayPeriodSegmenter segmenter = new(calendarToolsMock.Object);

        // Act
        PayPeriod[] payPeriods = [.. segmenter.GetPayPeriods(year)];

        // Assert
        Assert.Equal(Consts.PayPeriodsPerYear, payPeriods.Length);

        Assert.Equal(new DateTime(year, 1, 2), payPeriods[0].StartDate);
        Assert.Equal(new DateTime(year, 1, 15), payPeriods[0].EndDate);


        Assert.Equal(new DateTime(year, 12, 18), payPeriods[25].StartDate);
        Assert.Equal(new DateTime(year, 12, 31), payPeriods[25].EndDate);
    }
}
