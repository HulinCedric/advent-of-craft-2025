namespace SantaScheduling;

public static class SantaSchedulingArrivalCommand
{
    public static DateTime ComputeArrival(double tz)
        => new(
            year: 2024,
            month: 12,
            day: 24 + (tz < -5 ? 1 : 0),
            hour: tz < 0 ? 23 : 20,
            minute: 0,
            second: 0);
}