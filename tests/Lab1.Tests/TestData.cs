using Itmo.ObjectOrientedProgramming.Lab1.Entities;
using Itmo.ObjectOrientedProgramming.Lab1.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab1.Tests;

internal static class TestData
{
    public static readonly Sku Bolt = new(1, "болт", new Obx(1, 1));

    public static Estimate Manifest(int amount)
    {
        return new Estimate([new SkuQuantity(Bolt, amount)]);
    }

    public static TimeType Minutes(int minutes)
    {
        return new TimeType(TimeSpan.FromMinutes(minutes));
    }

    public static Employee Worker(int obx, int loadingMinutes, int transferMinutes)
    {
        return new Employee(new Obx(obx, obx), Minutes(loadingMinutes), Minutes(transferMinutes));
    }
}
