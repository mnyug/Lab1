using Itmo.ObjectOrientedProgramming.Lab1.Entities;
using Itmo.ObjectOrientedProgramming.Lab1.ValueObjects;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab1.Tests;

public class ProductInventoryTests
{
    [Theory]
    [InlineData(5, true)]
    [InlineData(10, true)]
    [InlineData(11, false)]
    public void RespectsCapacity(int amount, bool expected)
    {
        // Arrange
        var inventory = new ProductInventory(new Obx(10, 10));

        // Act
        bool fits = inventory.CanFit(TestData.Manifest(amount));

        // Assert
        Assert.Equal(expected, fits);
    }
}
