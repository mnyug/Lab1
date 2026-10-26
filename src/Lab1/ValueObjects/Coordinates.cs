namespace Itmo.ObjectOrientedProgramming.Lab1.ValueObjects;

public readonly record struct Coordinates(double Latitude, double Longitude)
{
    public Distance DistanceTo(Coordinates other)
    {
        double latitudeDelta = other.Latitude - Latitude;
        double longitudeDelta = other.Longitude - Longitude;

        return new Distance(Math.Sqrt((latitudeDelta * latitudeDelta) + (longitudeDelta * longitudeDelta)));
    }
}