using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab1.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities;

public class ShippingStage : IRouteStage
{
    private readonly Storage _storage;
    private readonly Estimate _manifest;

    public ShippingStage(Storage storage, Estimate manifest)
    {
        _storage = storage;
        _manifest = manifest;
    }

    public DistributionOperationResult Execute(Truck truck)
    {
        Distance distance = _storage.Coordinates.DistanceTo(truck.Coordinates);
        if (distance > Storage.MaximumTransferDistance)
        {
            return new DistributionOperationResult.ImpossibleDistance(distance);
        }

        if (!truck.CanAccept(_manifest))
        {
            return new DistributionOperationResult.ImpossibleToAccept(_manifest.Quantity);
        }

        DistributionOperationResult result = _storage.Ship(_manifest);
        if (result is DistributionOperationResult.Success)
        {
            truck.Load(_manifest);
        }

        return result;
    }
}
