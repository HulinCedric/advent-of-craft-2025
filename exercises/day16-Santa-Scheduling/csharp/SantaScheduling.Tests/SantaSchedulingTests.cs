using System.Diagnostics;
using Xunit;

namespace SantaScheduling.Tests;

public class SantaSchedulingTests
{
    /*
     * TODO: Before you can write meaningful tests, you'll need to extract
     * the scheduling logic from Program.cs into a testable method.
     */

    [Fact]
    public void ArrivalTestHarness()
    {
        var output = new StringWriter();
        Console.SetOut(output);

        SantaSchedulingApplication.Run(["a", "0"]);

        var consoleOutput = output.ToString();

        Assert.Equal($"Santa arrives: 24/12/2024 20:00:00{Environment.NewLine}", consoleOutput);
    }

    [Fact]
    public void DepartureTestHarness()
    {
        using var santaSchedule = Process.Start(
            new ProcessStartInfo
            {
                FileName = "SantaScheduling",
                Arguments = "l 0",
                RedirectStandardOutput = true
            })!;

        santaSchedule.WaitForExit();

        var consoleOutput = santaSchedule.StandardOutput.ReadToEnd();

        Assert.Equal($"Santa departs: 25/12/2024 02:00:00{Environment.NewLine}", consoleOutput);
    }

    [Fact]
    public void HelpTestHarness()
    {
        using var santaSchedule = Process.Start(
            new ProcessStartInfo
            {
                FileName = "SantaScheduling",
                Arguments = "",
                RedirectStandardOutput = true
            })!;

        santaSchedule.WaitForExit();

        var consoleOutput = santaSchedule.StandardOutput.ReadToEnd();

        Assert.Equal(
            $"Usage: SantaScheduling <command> <timezone>{Environment.NewLine}" +
            $"Commands:{Environment.NewLine}" +
            $"  a - Show arrival time{Environment.NewLine}" +
            $"  l - Show departure time{Environment.NewLine}" +
            $"Example: SantaScheduling a -5{Environment.NewLine}",
            consoleOutput);
    }

    [Fact]
    public void UnknownCommandTestHarness()
    {
        using var santaSchedule = Process.Start(
            new ProcessStartInfo
            {
                FileName = "SantaScheduling",
                Arguments = "u 0",
                RedirectStandardOutput = true
            })!;

        santaSchedule.WaitForExit();

        var consoleOutput = santaSchedule.StandardOutput.ReadToEnd();

        Assert.Equal($"Unknown command: u{Environment.NewLine}", consoleOutput);
    }

    [Fact(DisplayName = "TICKET-102: Investigation - Compare arrival times")]
    public void Ticket102_Investigation()
    {
        // After refactoring, investigate:
        // - London (UTC+0) arrival time
        // - New York (UTC-5) arrival time
        // - Why the 3-hour difference?

        Assert.True(true, "Extract the logic first, then investigate");
    }

    [Fact(DisplayName = "TICKET-103: Investigation - Test boundary points")]
    public void Ticket103_Investigation()
    {
        // After refactoring, test:
        // - What happens at exactly -5?
        // - What happens at exactly 0?
        // - Are they grouped with the zones before or after?

        Assert.True(true, "Make it testable first");
    }

    [Fact(DisplayName = "TICKET-104: Investigation - Mumbai and Newfoundland")]
    public void Ticket104_Investigation()
    {
        // After refactoring, test:
        // - Mumbai: UTC+5.5
        // - Newfoundland: UTC-3.5
        // - How are half-hour offsets handled?

        Assert.True(true, "Refactor, then investigate");
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
}