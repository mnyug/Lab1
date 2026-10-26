using Itmo.ObjectOrientedProgramming.Lab1.Entities;
using Itmo.ObjectOrientedProgramming.Lab1.Services;
using Itmo.ObjectOrientedProgramming.Lab1.ValueObjects;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab1.Tests;

public class StorageTests
{
    [Fact]
    public void EmptyCargoNeedsNoEmployeesOrTime()
    {
        // Arrange
        var storage = new Storage(new Coordinates(0, 0), new Obx(10, 10), [], new SelectStrategy());
        var cargo = new Estimate([]);

        // Act
        DistributionOperationResult received = storage.Receive(cargo);
        DistributionOperationResult shipped = storage.Ship(cargo);
        DistributionOperationResult withoutEmployees = storage.Receive(TestData.Manifest(1));

        // Assert
        Assert.Equal(TimeType.Zero, Assert.IsType<DistributionOperationResult.Success>(received).Time);
        Assert.Equal(TimeType.Zero, Assert.IsType<DistributionOperationResult.Success>(shipped).Time);
        Assert.IsType<DistributionOperationResult.ImpossibleToAllocateTheStaff>(withoutEmployees);
    }

    [Fact]
    public void FailedReceiveDoesNotPartiallyAcceptAssignableCargo()
    {
        // Arrange
        var storage = new Storage(new Coordinates(0, 0), new Obx(100, 100), [TestData.Worker(5, 1, 2)], new SelectStrategy());
        var heavy = new Sku(2, "тяжёлый", new Obx(6, 1));
        var cargo = new Estimate([new SkuQuantity(TestData.Bolt, 5), new SkuQuantity(heavy, 1)]);

        // Act
        DistributionOperationResult result = storage.Receive(cargo);

        // Assert
        Assert.IsType<DistributionOperationResult.ImpossibleToAllocateTheStaff>(result);
        Assert.False(storage.CanRelease(TestData.Manifest(1)));
        Assert.True(storage.CanAccept(TestData.Manifest(100)));
    }

    [Fact]
    public void ReceivesAndShipsCargoWithoutTruckDependency()
    {
        // Arrange
        var storage = new Storage(new Coordinates(0, 0), new Obx(10, 10), [TestData.Worker(5, 1, 2)], new SelectStrategy());
        Estimate cargo = TestData.Manifest(10);

        // Act
        DistributionOperationResult receiveResult = storage.Receive(cargo);

        // Assert
        DistributionOperationResult.Success received = Assert.IsType<DistributionOperationResult.Success>(receiveResult);
        Assert.Equal(TimeSpan.FromMinutes(6), received.Time.Value);
        Assert.True(storage.CanRelease(cargo));

        // Act
        DistributionOperationResult shipResult = storage.Ship(cargo);

        // Assert
        DistributionOperationResult.Success shipped = Assert.IsType<DistributionOperationResult.Success>(shipResult);
        Assert.Equal(TimeSpan.FromMinutes(6), shipped.Time.Value);
        Assert.False(storage.CanRelease(cargo));
        Assert.True(storage.CanAccept(cargo));
    }
}
