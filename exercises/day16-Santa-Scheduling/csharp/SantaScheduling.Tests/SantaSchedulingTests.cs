using Xunit;

namespace SantaScheduling.Tests;

public class SantaSchedulingTests
{
    [Fact]
    public void Should_return_santa_arrival_date()
    {
        var consoleOutput = SantaSchedulingArrival(timezone: 0);

        Assert.Equal($"Santa arrives: 24/12/2024 20:00:00{Environment.NewLine}", consoleOutput);
    }

    [Fact]
    public void Should_return_santa_departure_date()
    {
        var consoleOutput = SantaSchedulingDeparture(timezone: 0);

        Assert.Equal($"Santa departs: 25/12/2024 02:00:00{Environment.NewLine}", consoleOutput);
    }

    [Fact]
    public void Should_return_help_on_missing_command()
    {
        var consoleOutput = RunSantaSchedulingApplication([]);

        Assert.Equal(
            $"Usage: SantaScheduling <command> <timezone>{Environment.NewLine}" +
            $"Commands:{Environment.NewLine}" +
            $"  a - Show arrival time{Environment.NewLine}" +
            $"  l - Show departure time{Environment.NewLine}" +
            $"Example: SantaScheduling a -5{Environment.NewLine}",
            consoleOutput);
    }

    [Fact]
    public void Should_return_failure_on_unknown_command()
    {
        var consoleOutput = RunSantaSchedulingApplication(["u", "0"]);

        Assert.Equal($"Unknown command: u{Environment.NewLine}", consoleOutput);
    }

    [Theory]
    [ClassData(typeof(WesternZones))]
    public void Should_arrive_the_25th_at_11pm_in_western_timezones(TimeZoneInfo timezone)
    {
        var arrivalTime = SantaSchedulingArrivalCommand.ComputeArrival(timezone.BaseUtcOffset.TotalHours);

        Assert.Equal("25/12/2024 23:00:00", $"{arrivalTime:dd/MM/yyyy HH:mm:ss}");
    }

    [Theory]
    [ClassData(typeof(CentralZones))]
    public void Should_arrive_the_24th_at_11pm_in_central_timezones(TimeZoneInfo timezone)
    {
        var arrivalTime = SantaSchedulingArrivalCommand.ComputeArrival(timezone.BaseUtcOffset.TotalHours);

        Assert.Equal("24/12/2024 23:00:00", $"{arrivalTime:dd/MM/yyyy HH:mm:ss}");
    }

    [Theory]
    [ClassData(typeof(EasternZones))]
    public void Should_arrive_the_24th_at_8pm_in_eastern_timezones(TimeZoneInfo timezone)
    {
        var arrivalTime = SantaSchedulingArrivalCommand.ComputeArrival(timezone.BaseUtcOffset.TotalHours);

        Assert.Equal("24/12/2024 20:00:00", $"{arrivalTime:dd/MM/yyyy HH:mm:ss}");
    }

    private static string SantaSchedulingArrival(int timezone) => RunSantaSchedulingApplication(["a", $"{timezone}"]);

    private static string SantaSchedulingDeparture(int timezone) => RunSantaSchedulingApplication(["l", $"{timezone}"]);

    private static string RunSantaSchedulingApplication(string[] args)
    {
        var output = new StringWriter();
        Console.SetOut(output);

        SantaSchedulingApplication.Run(args);

        return output.ToString();
    }
}

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