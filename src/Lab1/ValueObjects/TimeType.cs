namespace Itmo.ObjectOrientedProgramming.Lab1.ValueObjects;

public readonly record struct TimeType
{
    public TimeType(TimeSpan value)
    {
        if (value < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(value), "Длительность не может быть отрицательной ");

        Value = value;
    }

    public static TimeType Zero => new(TimeSpan.Zero);

    public static TimeType FromHours(double hours) => new(TimeSpan.FromHours(hours));

    public static TimeType FromMinutes(double minutes) => new(TimeSpan.FromMinutes(minutes));

    public TimeSpan Value { get; }

    public static TimeType operator +(TimeType left, TimeType right) => new(left.Value + right.Value);

    public static bool operator <(TimeType left, TimeType right) => left.Value < right.Value;

    public static bool operator >(TimeType left, TimeType right) => left.Value > right.Value;

    public override string ToString() => Value.ToString();
}
