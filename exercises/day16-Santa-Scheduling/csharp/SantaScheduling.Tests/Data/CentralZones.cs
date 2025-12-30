using Xunit;

namespace SantaScheduling.Tests.Data;

public class CentralZones : TheoryData<TimeZoneInfo>
{
    public CentralZones()
    {
        foreach (var timeZoneInfo in TimeZoneInfo.GetSystemTimeZones()
                     .Where(tz => tz.BaseUtcOffset >= TimeSpan.FromHours(-5.0) &&
                                  tz.BaseUtcOffset < TimeSpan.FromHours(+0.0)))
        {
            Add(timeZoneInfo);
        }
    }
}