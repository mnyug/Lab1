using Itmo.ObjectOrientedProgramming.Lab1.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities;

public class Estimate
{
    private readonly Dictionary<Sku, int> _estimate;

    public Estimate(IReadOnlyList<SkuQuantity> positions)
    {
        ArgumentNullException.ThrowIfNull(positions);

        _estimate = new Dictionary<Sku, int>();

        foreach (SkuQuantity position in positions)
            _estimate[position.Sku] = _estimate.GetValueOrDefault(position.Sku) + position.Quantity;
    }

    public int Quantity => _estimate.Values.Sum();

    public IReadOnlyList<SkuQuantity> Positions =>
        _estimate.Select(pair => new SkuQuantity(pair.Key, pair.Value)).ToList();

    public Obx TotalWeight =>
        _estimate.Aggregate(Obx.Zero, (total, position) => total + (position.Key.Weight * position.Value));

    public EstimateSplit Take(Obx limitObx)
    {
        var taken = new List<SkuQuantity>();
        var rest = new List<SkuQuantity>();

        foreach ((Sku? sku, int amount) in _estimate)
        {
            int amountForTransfer = Math.Min(amount, limitObx / sku.Weight);

            if (amountForTransfer > 0)
            {
                taken.Add(new SkuQuantity(sku, amountForTransfer));
                limitObx -= amountForTransfer * sku.Weight;
            }

            int remaining = amount - amountForTransfer;

            if (remaining > 0)
                rest.Add(new SkuQuantity(sku, remaining));
        }

        return new EstimateSplit(new Estimate(taken), new Estimate(rest));
    }
}