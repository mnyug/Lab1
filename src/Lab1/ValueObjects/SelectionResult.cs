using Itmo.ObjectOrientedProgramming.Lab1.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab1.ValueObjects;

public sealed record SelectionResult(IReadOnlyList<Assignment> Assigned, Estimate Unassigned);