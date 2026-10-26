using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab1.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities;

public class Storage
{
    public static readonly Distance MaximumTransferDistance = new Distance(0.01);
    private readonly ProductInventory _products;
    private readonly IReadOnlyList<Employee> _employees;
    private readonly ISelectStrategy _selectStrategy;

    public Storage(Coordinates coordinates, Obx holdingCapacity, IReadOnlyList<Employee> employees, ISelectStrategy selectStrategy)
    {
        Coordinates = coordinates;
        _selectStrategy = selectStrategy;
        _employees = new List<Employee>(employees);

        _products = new ProductInventory(holdingCapacity);
    }

    public Coordinates Coordinates { get; }

    public bool CanAccept(Estimate estimate)
    {
        return _products.CanFit(estimate);
    }

    public bool CanRelease(Estimate estimate) => _products.Exists(estimate);

    public DistributionOperationResult Receive(Estimate smeta)
    {
        if (!CanAccept(smeta))
        {
            return new DistributionOperationResult.ImpossibleToAccept(smeta.Quantity);
        }

        DistributionOperationResult result = ProcessWork(smeta);
        if (result is DistributionOperationResult.Success)
        {
            _products.AddProduct(smeta);
        }

        return result;
    }

    public DistributionOperationResult Ship(Estimate smeta)
    {
        if (!CanRelease(smeta))
        {
            return new DistributionOperationResult.ImpossibleToRelease(smeta.Quantity);
        }

        DistributionOperationResult result = ProcessWork(smeta);
        if (result is DistributionOperationResult.Success)
        {
            _products.RemoveProduct(smeta);
        }

        return result;
    }

    private DistributionOperationResult ProcessWork(Estimate smeta)
    {
        SelectionResult selection = _selectStrategy.Distribute(_employees, smeta);

        if (selection.Unassigned.Quantity > 0)
            return new DistributionOperationResult.ImpossibleToAllocateTheStaff();

        TimeType operationTime = TimeType.Zero;

        foreach (Assignment assignment in selection.Assigned)
        {
            TimeType employeeTime = TimeType.Zero;

            foreach (Estimate trip in assignment.Trips)
            {
                employeeTime += assignment.Employee.Work(trip);
            }

            if (operationTime < employeeTime)
            {
                operationTime = employeeTime;
            }
        }

        return new DistributionOperationResult.Success(operationTime);
    }
}
