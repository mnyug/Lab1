using Itmo.ObjectOrientedProgramming.Lab1.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities;

public record Employee
{
    private readonly TimeType _transferTime;
    private readonly TimeType _loadingUnloadingTime;

    public Employee(Obx obx, TimeType loadingUnloadingTime, TimeType transferTime)
    {
        if (obx == Obx.Zero)
            throw new ArgumentOutOfRangeException(nameof(obx), "Переносимые ОВХ должны быть положительными ");
        if (loadingUnloadingTime == TimeType.Zero)
            throw new ArgumentOutOfRangeException(nameof(loadingUnloadingTime), "Время погрузки должно быть положительным ");
        if (transferTime == TimeType.Zero)
            throw new ArgumentOutOfRangeException(nameof(transferTime), "Время переноса должно быть положительным ");
        CarryingCapacity = obx;
        _transferTime = transferTime;
        _loadingUnloadingTime = loadingUnloadingTime;
    }

    public Obx CarryingCapacity { get; }

    public TimeType TripTime => _loadingUnloadingTime + _transferTime;

    public TimeType Work(Estimate cargo)
    {
        return TripTime;
    }
}