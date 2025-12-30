using Xunit;

namespace SantaScheduling.Tests.Data;

public class WesternZones : TheoryData<TimeZoneInfo>
{
    public WesternZones()
    {
        foreach (var timeZoneInfo in TimeZoneInfo.GetSystemTimeZones()
                     .Where(tz => tz.BaseUtcOffset < TimeSpan.FromHours(-5.0)))
        {
            Add(timeZoneInfo);
        }
    }
}