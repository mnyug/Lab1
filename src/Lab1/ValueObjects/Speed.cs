namespace Itmo.ObjectOrientedProgramming.Lab1.ValueObjects;

public readonly record struct Speed
{
    public Speed(int kilometersPerHour)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(kilometersPerHour);

        KilometersPerHour = kilometersPerHour;
    }

    public int KilometersPerHour { get; }
}