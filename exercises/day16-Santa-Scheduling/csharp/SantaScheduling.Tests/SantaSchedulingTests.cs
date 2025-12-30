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

    [Fact(DisplayName = "TICKET-102: Investigation - Compare arrival times")]
    public void Ticket102_Investigation()
    {
        // After refactoring, investigate:
        // - London (UTC+0) arrival time
        // - New York (UTC-5) arrival time
        // - Why the 3-hour difference?

        var londonArrivalTime = SantaSchedulingArrivalCommand.ComputeArrival(timezone: 0);

        Assert.Equal("24/12/2024 20:00:00", $"{londonArrivalTime}");

        var newYorkArrivalTime = SantaSchedulingArrivalCommand.ComputeArrival(timezone: -5);

        Assert.Equal("24/12/2024 23:00:00", $"{newYorkArrivalTime}");
    }

    [Theory]
    [InlineData(-7, "25/12/2024 23:00:00")]
    [InlineData(-6, "25/12/2024 23:00:00")]
    [InlineData(-5, "24/12/2024 23:00:00")]
    [InlineData(-4, "24/12/2024 23:00:00")]
    [InlineData(-1, "24/12/2024 23:00:00")]
    [InlineData(0, "24/12/2024 20:00:00")]
    [InlineData(1, "24/12/2024 20:00:00")]
    public void Ticket103_Investigation(double timezone, string expectedArrival)
    {
        // After refactoring, test:
        // - What happens at exactly -5?
        // - What happens at exactly 0?
        // - Are they grouped with the zones before or after?

        var arrivalTime = SantaSchedulingArrivalCommand.ComputeArrival(timezone);

        Assert.Equal(expectedArrival, $"{arrivalTime}");
    }

    [Theory(DisplayName = "TICKET-104: Investigation - Mumbai and Newfoundland")]
    [InlineData(+5.5, "24/12/2024 20:00:00")]
    [InlineData(+0.1, "24/12/2024 20:00:00")]
    [InlineData(+0, "24/12/2024 20:00:00")]
    [InlineData(-0.1, "24/12/2024 23:00:00")]
    [InlineData(-3.5, "24/12/2024 23:00:00")]
    [InlineData(-5.0, "24/12/2024 23:00:00")]
    [InlineData(-5.1, "25/12/2024 23:00:00")]
    public void Ticket104_Investigation(double timezone, string expectedArrival)
    {
        // After refactoring, test:
        // - Mumbai: UTC+5.5
        // - Newfoundland: UTC-3.5
        // - How are half-hour offsets handled?

        var arrivalTime = SantaSchedulingArrivalCommand.ComputeArrival(timezone);

        Assert.Equal(expectedArrival, $"{arrivalTime}");
    }

    [Fact(DisplayName = "TICKET-105: Investigation - Map all regions")]
    public void Ticket105_Investigation()
    {
        // After refactoring, document:
        // - How many different rules are there?
        // - What timezone ranges does each rule cover?
        // - UTC-12 to UTC+14 - what's the complete picture?

        Assert.True(true, "Extract logic, then map the rules");
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