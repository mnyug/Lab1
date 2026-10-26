namespace Itmo.ObjectOrientedProgramming.Lab1.ValueObjects;

public readonly record struct Distance
{
    public Distance(double kilometers)
    {
        Kilometers = kilometers;
    }

    public double Kilometers { get; }

    public static bool operator <(Distance left, Distance right) => left.Kilometers < right.Kilometers;

    public static bool operator >(Distance left, Distance right) => left.Kilometers > right.Kilometers;

    public static bool operator <=(Distance left, Distance right) => left.Kilometers <= right.Kilometers;

    public static bool operator >=(Distance left, Distance right) => left.Kilometers >= right.Kilometers;
}