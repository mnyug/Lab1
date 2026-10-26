using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab1.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities;

public class ReceivingStage : IRouteStage
{
    private readonly Storage _storage;
    private readonly Estimate _manifest;

    public ReceivingStage(Storage storage, Estimate manifest)
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

        if (!truck.CanRelease(_manifest))
        {
            return new DistributionOperationResult.ImpossibleToRelease(_manifest.Quantity);
        }

        truck.Unload(_manifest);

        DistributionOperationResult result = _storage.Receive(_manifest);

        if (result is not DistributionOperationResult.Success)
        {
            truck.Load(_manifest);
        }

        return result;
    }
}
