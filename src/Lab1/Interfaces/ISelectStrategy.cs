using Itmo.ObjectOrientedProgramming.Lab1.Entities;
using Itmo.ObjectOrientedProgramming.Lab1.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab1.Interfaces;

public interface ISelectStrategy
{
    SelectionResult Distribute(IReadOnlyList<Employee> employees, Estimate estimate);
}