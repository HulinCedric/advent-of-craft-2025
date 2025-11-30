namespace Day01;

internal static class DateExtensions
{
    internal static bool HasNotExpired(this DateOnly expirationDate, DateOnly now) => expirationDate > now;
}