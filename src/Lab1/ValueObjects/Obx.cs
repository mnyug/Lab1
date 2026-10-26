namespace Itmo.ObjectOrientedProgramming.Lab1.ValueObjects;

public readonly record struct Obx
{
    public Obx(int weight, int volume)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(weight);
        ArgumentOutOfRangeException.ThrowIfNegative(volume);

        Weight = weight;
        Volume = volume;
    }

    public static Obx Zero => new(0, 0);

    public int Weight { get; }

    public int Volume { get; }

    public static Obx operator +(Obx left, Obx right) =>
        new(left.Weight + right.Weight, left.Volume + right.Volume);

    public static Obx operator -(Obx left, Obx right) =>
        new(left.Weight - right.Weight, left.Volume - right.Volume);

    public static Obx operator *(Obx obx, int multiplier) =>
        new(obx.Weight * multiplier, obx.Volume * multiplier);

    public static Obx operator *(int multiplier, Obx obx) => obx * multiplier;

    public static int operator /(Obx capacity, Obx item)
    {
        int byWeight = int.MaxValue;
        int byVolume = int.MaxValue;

        if (item.Weight != 0)
        {
            byWeight = capacity.Weight / item.Weight;
        }

        if (item.Volume != 0)
        {
            byVolume = capacity.Volume / item.Volume;
        }

        return Math.Min(byWeight, byVolume);
    }

    public static bool operator <=(Obx left, Obx right)
    {
        return left.Weight <= right.Weight
               && left.Volume <= right.Volume;
    }

    public static bool operator >=(Obx left, Obx right)
    {
        return left.Weight >= right.Weight
               && left.Volume >= right.Volume;
    }
}