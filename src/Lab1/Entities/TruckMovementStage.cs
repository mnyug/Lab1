using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab1.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities;

public class TruckMovementStage : IRouteStage
{
    private readonly Coordinates _coordinates;

    public TruckMovementStage(Coordinates coordinates)
    {
        _coordinates = coordinates;
    }

    public DistributionOperationResult Execute(Truck truck) =>
        new DistributionOperationResult.Success(truck.Move(_coordinates));
}