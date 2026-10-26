using Itmo.ObjectOrientedProgramming.Lab1.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab1.ValueObjects;

public readonly record struct SkuQuantity
{
    public SkuQuantity(Sku sku, int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

        Sku = sku;
        Quantity = quantity;
    }

    public Sku Sku { get; }

    public int Quantity { get; }
}
