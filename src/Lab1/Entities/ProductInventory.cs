using Itmo.ObjectOrientedProgramming.Lab1.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities;

public class ProductInventory
{
    private readonly Obx _holdingCapacity;
    private readonly Dictionary<Sku, int> _products;

    public ProductInventory(Obx holdingCapacity)
    {
        if (holdingCapacity == Obx.Zero)
            throw new ArgumentOutOfRangeException(nameof(holdingCapacity), "Вместимость должна быть положительной ");

        _holdingCapacity = holdingCapacity;
        _products = new Dictionary<Sku, int>();
    }

    public Obx TotalWeight =>
        _products.Aggregate(Obx.Zero, (total, position) => total + (position.Key.Weight * position.Value));

    public bool CanFit(Estimate estimate) => TotalWeight + estimate.TotalWeight <= _holdingCapacity;

    public void AddProduct(Estimate estimate)
    {
        foreach (SkuQuantity position in estimate.Positions)
            _products[position.Sku] = _products.GetValueOrDefault(position.Sku) + position.Quantity;
    }

    public void RemoveProduct(Estimate estimate)
    {
        foreach (SkuQuantity position in estimate.Positions)
        {
            int remaining = _products[position.Sku] - position.Quantity;

            if (remaining == 0)
            {
                _products.Remove(position.Sku);
            }
            else
            {
                _products[position.Sku] = remaining;
            }
        }
    }

    public bool Exists(Estimate estimate)
    {
        foreach (SkuQuantity position in estimate.Positions)
        {
            if (!_products.TryGetValue(position.Sku, out int available) || available < position.Quantity)
                return false;
        }

        return true;
    }
}