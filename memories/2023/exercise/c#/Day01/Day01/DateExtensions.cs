namespace Day01;

internal static class DateExtensions
{
    internal static bool IsPassed(this DateOnly expirationDate, DateOnly date) => expirationDate > date;
}