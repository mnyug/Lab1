using Itmo.ObjectOrientedProgramming.Lab1.Entities;
using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab1.Services;
using Itmo.ObjectOrientedProgramming.Lab1.ValueObjects;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab1.Tests;

public class RouteSheetTests
{
    [Fact]
    public void StopsAtFirstFailure()
    {
        // Arrange
        Employee[] staff = [TestData.Worker(10, 1, 5)];
        var storage = new Storage(new Coordinates(50, 0), new Obx(1000, 1000), staff, new SelectStrategy());
        var truck = new Truck(new Obx(100, 100), new Speed(60), new Coordinates(0, 0));

        var sheet = new RouteSheet(new IRouteStage[]
        {
            new TruckMovementStage(new Coordinates(10, 0)),
            new ReceivingStage(storage, TestData.Manifest(5)),
            new TruckMovementStage(new Coordinates(100, 0)),
        });

        // Act
        DistributionOperationResult result = sheet.Execute(truck);

        // Assert
        Assert.IsType<DistributionOperationResult.ImpossibleDistance>(result);
        Assert.Equal(new Coordinates(10, 0), truck.Coordinates);
    }
}
