using Itmo.ObjectOrientedProgramming.Lab1.Entities;
using Itmo.ObjectOrientedProgramming.Lab1.Services;
using Itmo.ObjectOrientedProgramming.Lab1.ValueObjects;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab1.Tests;

public class SelectStrategyTests
{
    private static readonly Sku Heavy = new(2, "станок", new Obx(100, 100));

    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(11)]
    public void LeavesAllUnassigned_WhenNobodyCanCarry(int amount)
    {
        // Arrange
        Employee employee = TestData.Worker(obx: 10, loadingMinutes: 1, transferMinutes: 5);
        var manifest = new Estimate([new SkuQuantity(Heavy, amount)]);
        var strategy = new SelectStrategy();

        // Act
        SelectionResult result = strategy.Distribute([employee], manifest);

        // Assert
        Assert.Empty(result.Assigned.Single().Trips);
        Assert.Equal(amount, result.Unassigned.Quantity);
    }
}
