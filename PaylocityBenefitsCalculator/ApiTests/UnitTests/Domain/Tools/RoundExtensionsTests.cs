using Xunit;
using Api.Domain.Tools;

namespace Api.Domain.PaycheckCalculator.Tests;

public class RoundExtensionsTests
{
    [Theory]
    [InlineData(1.5, 1.5)]
    [InlineData(1.499, 1.5)]
    [InlineData(1.501, 1.5)]
    public void RoundTest(decimal testedValue, decimal expectedValue)
    {
        // act
        decimal rounded = testedValue.Round();

        // assert
        Assert.Equal(expectedValue, rounded);
    }
}
