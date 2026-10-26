using Itmo.ObjectOrientedProgramming.Lab1.Entities;
using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab1.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab1.Services;

public class SelectStrategy : ISelectStrategy
{
    public SelectionResult Distribute(IReadOnlyList<Employee> employees, Estimate estimate)
    {
        var trips = employees.ToDictionary(employee => employee, _ => new List<Estimate>());
        Estimate rest = estimate;
        while (rest.Quantity > 0)
        {
            int quantityBeforeRound = rest.Quantity;
            foreach (Employee employee in employees)
            {
                EstimateSplit split = rest.Take(employee.CarryingCapacity);

                if (split.Taken.Quantity == 0)
                    continue;

                trips[employee].Add(split.Taken);
                rest = split.Rest;
            }

            if (quantityBeforeRound == rest.Quantity)
            {
                break;
            }
        }

        var assigned = employees
            .Select(employee => new Assignment(employee, trips[employee]))
            .ToList();

        return new SelectionResult(assigned, rest);
    }
}