namespace SantaScheduling;

public static class SantaSchedulingArrivalCommand
{
    public static DateTime ComputeArrival(double timezone)
        => new(
            year: 2024,
            month: 12,
            day: 24 + (timezone < -5 ? 1 : 0),
            hour: timezone < 0 ? 23 : 20,
            minute: 0,
            second: 0);
}