namespace Itmo.ObjectOrientedProgramming.Lab1.ValueObjects;

public abstract record DistributionOperationResult
{
    private DistributionOperationResult() { }

    public sealed record Success(TimeType Time) : DistributionOperationResult;

    public sealed record ImpossibleDistance(Distance Distance) : DistributionOperationResult;

    public sealed record ImpossibleToRelease(int Quantity) : DistributionOperationResult;

    public sealed record ImpossibleToAccept(int Quantity) : DistributionOperationResult;

    public sealed record ImpossibleToAllocateTheStaff : DistributionOperationResult;
}