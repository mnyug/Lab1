using Itmo.ObjectOrientedProgramming.Lab1.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab1.ValueObjects;

public sealed record Assignment(Employee Employee, IReadOnlyList<Estimate> Trips);