using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab1.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities;

public class RouteSheet
{
    private readonly IReadOnlyList<IRouteStage> _stages;

    public RouteSheet(IReadOnlyList<IRouteStage> stages)
    {
        _stages = stages;
    }

    public DistributionOperationResult Execute(Truck truck)
    {
        TimeType total = TimeType.Zero;

        foreach (IRouteStage stage in _stages)
        {
            DistributionOperationResult result = stage.Execute(truck);

            if (result is not DistributionOperationResult.Success success)
                return result;

            total += success.Time;
        }

        return new DistributionOperationResult.Success(total);
    }
}