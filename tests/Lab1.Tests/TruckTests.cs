using Itmo.ObjectOrientedProgramming.Lab1.Entities;
using Itmo.ObjectOrientedProgramming.Lab1.ValueObjects;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab1.Tests;

public class TruckTests
{
    [Fact]
    public void MovesAndReturnsTime()
    {
        // Arrange
        var truck = new Truck(new Obx(100, 100), new Speed(60), new Coordinates(0, 0));

        // Act
        TimeType elapsed = truck.Move(new Coordinates(100, 0));

        // Assert
        Assert.Equal(TimeSpan.FromHours(100d / 60), elapsed.Value);
        Assert.Equal(new Coordinates(100, 0), truck.Coordinates);
    }
}
