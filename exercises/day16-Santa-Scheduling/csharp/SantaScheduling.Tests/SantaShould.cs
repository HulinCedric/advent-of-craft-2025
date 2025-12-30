using FluentAssertions;
using SantaScheduling.Tests.Data;
using SantaScheduling.Tests.Extensions;
using Xunit;
using static SantaScheduling.SantaSchedulingArrivalCommand;

namespace SantaScheduling.Tests;

public class SantaShould
{
    [Theory]
    [ClassData(typeof(WesternZones))]
    public void Arrive_the_25th_at_11pm_in_western_timezones(TimeZoneInfo timezone)
        => SantaArrivalTimeIn(timezone).Should().Be("12/25/2024 23:00:00");

    [Theory]
    [ClassData(typeof(CentralZones))]
    public void Arrive_the_24th_at_11pm_in_central_timezones(TimeZoneInfo timezone)
        => SantaArrivalTimeIn(timezone).Should().Be("12/24/2024 23:00:00");

    [Theory]
    [ClassData(typeof(EasternZones))]
    public void Arrive_the_24th_at_8pm_in_eastern_timezones(TimeZoneInfo timezone)
        => SantaArrivalTimeIn(timezone).Should().Be("12/24/2024 20:00:00");

    private static string SantaArrivalTimeIn(TimeZoneInfo timezone)
        => PrintArrivalTime(ComputeArrival(timezone.ToLegacyTimezone()));

    private static string PrintArrivalTime(DateTime arrivalTime) => $"{arrivalTime:MM/dd/yyyy HH:mm:ss}";
}