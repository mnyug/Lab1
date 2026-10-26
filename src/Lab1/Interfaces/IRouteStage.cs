using Itmo.ObjectOrientedProgramming.Lab1.Entities;
using Itmo.ObjectOrientedProgramming.Lab1.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab1.Interfaces;

public interface IRouteStage
{
    DistributionOperationResult Execute(Truck truck);
}