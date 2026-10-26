using Itmo.ObjectOrientedProgramming.Lab1.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities;

public class Truck
{
    private readonly Speed _speed;
    private readonly ProductInventory _products;

    public Truck(Obx holdingCapacity, Speed speed, Coordinates coordinates)
    {
        _speed = speed;
        Coordinates = coordinates;

        _products = new ProductInventory(holdingCapacity);
    }

    public Coordinates Coordinates { get; private set; }

    public bool CanAccept(Estimate estimate) => _products.CanFit(estimate);

    public bool CanRelease(Estimate estimate) => _products.Exists(estimate);

    public void Load(Estimate estimate) => _products.AddProduct(estimate);

    public void Unload(Estimate estimate) => _products.RemoveProduct(estimate);

    public TimeType Move(Coordinates destination)
    {
        Distance distance = Coordinates.DistanceTo(destination);
        Coordinates = destination;
        return TimeType.FromHours(distance.Kilometers / _speed.KilometersPerHour);
    }
}
