namespace SantaScheduling.Tests.Extensions;

public static class TimeZoneConverterExtensions
{
    public static double ToLegacyTimezone(this TimeZoneInfo timezone) => timezone.BaseUtcOffset.TotalHours;
}