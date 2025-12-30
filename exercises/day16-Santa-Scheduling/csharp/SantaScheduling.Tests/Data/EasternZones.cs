using Xunit;

namespace SantaScheduling.Tests.Data;

public class EasternZones : TheoryData<TimeZoneInfo>
{
    public EasternZones()
    {
        foreach (var timeZoneInfo in TimeZoneInfo.GetSystemTimeZones()
                     .Where(tz => tz.BaseUtcOffset >= TimeSpan.FromHours(+0.0)))
        {
            Add(timeZoneInfo);
        }
    }
}