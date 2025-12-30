using System.Globalization;
using Xunit;

namespace SantaScheduling.Tests;

public class SantaSchedulingTests
{
    public SantaSchedulingTests() => CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

    [Fact]
    public void Should_return_santa_arrival_date()
    {
        var consoleOutput = SantaSchedulingArrival(timezone: 0);

        Assert.Equal($"Santa arrives: 12/24/2024 20:00:00{Environment.NewLine}", consoleOutput);
    }

    [Fact]
    public void Should_return_santa_departure_date()
    {
        var consoleOutput = SantaSchedulingDeparture(timezone: 0);

        Assert.Equal($"Santa departs: 12/25/2024 02:00:00{Environment.NewLine}", consoleOutput);
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