using Itmo.ObjectOrientedProgramming.Lab1.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities;

public record Sku
{
    public Sku(int id, string name, Obx weight)
    {
        if (weight <= Obx.Zero)
            throw new ArgumentOutOfRangeException(nameof(weight), "ОВХ товара должны быть положительными ");

        Id = id;
        Name = name;
        Weight = weight;
    }

    public int Id { get; }

    public string Name { get; }

    public Obx Weight { get; }
}