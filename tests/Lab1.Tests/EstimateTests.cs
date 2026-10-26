using Itmo.ObjectOrientedProgramming.Lab1.Entities;
using Itmo.ObjectOrientedProgramming.Lab1.ValueObjects;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab1.Tests;

public class EstimateTests
{
    [Theory]
    [InlineData(20, 10, 0)]
    [InlineData(10, 10, 0)]
    [InlineData(5, 5, 5)]
    [InlineData(1, 1, 9)]
    [InlineData(0, 0, 10)]
    public void SplitsByLimit(int limit, int expectedTaken, int expectedRest)
    {
        // Arrange
        Estimate manifest = TestData.Manifest(10);

        // Act
        EstimateSplit result = manifest.Take(new Obx(limit, limit));

        // Assert
        Assert.Equal(expectedTaken, result.Taken.Quantity);
        Assert.Equal(expectedRest, result.Rest.Quantity);
        Assert.Equal(10, result.Taken.Quantity + result.Rest.Quantity);
    }
}