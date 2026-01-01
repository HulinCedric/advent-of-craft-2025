using FluentAssertions;
using static ControlSystem.Tests.SleighBuilder;

namespace ControlSystem.Tests;

public class TestControlSystem : IDisposable
{
    private readonly TextWriter _originalOutput;
    private readonly StringWriter _output;

    public TestControlSystem()
    {
        _output = new StringWriter();
        _originalOutput = Console.Out;
        Console.SetOut(_output);
    }

    public void Dispose()
    {
        Console.SetOut(_originalOutput);
        _output.Dispose();
    }

    [Fact]
    public void TestStart()
    {
        var controlSystem = new Core.ControlSystem(ASleigh());

        controlSystem.StartSystem();

        _output.ToString()
            .Trim()
            .Should()
            .Be(
                """
                Starting the sleigh...
                System ready.
                """);
    }

    [Fact]
    public void TestAlreadyStart()
    {
        var controlSystem = new Core.ControlSystem(ASleigh().On());

        controlSystem.StartSystem();

        _output.ToString()
            .Trim()
            .Should()
            .Be(
                """
                Starting the sleigh...
                Cannot turn on the sleigh because it is already on.
                """);
    }

    [Fact]
    public void TestAscend()
    {
        var controlSystem = new Core.ControlSystem(ASleigh().Off());
        controlSystem.StartSystem();
        controlSystem.Ascend();
        _output.ToString()
            .Trim()
            .Should()
            .Be(
                """
                Starting the sleigh...
                System ready.
                Ascending...
                """);
    }

    [Fact]
    public void TestDescend()
    {
        var controlSystem = new Core.ControlSystem(ASleigh().Off());
        controlSystem.StartSystem();
        controlSystem.Ascend();
        controlSystem.Descend();
        _output.ToString()
            .Trim()
            .Should()
            .Be(
                """
                Starting the sleigh...
                System ready.
                Ascending...
                Descending...
                """);
    }

    [Fact]
    public void TestDescendWhenParked()
    {
        var controlSystem = new Core.ControlSystem(ASleigh().On().Parked());

        controlSystem.Descend();

        _output.ToString().Trim().Should().Be("The sleigh must be flying to descend.");
    }

    [Fact]
    public void TestDescendWhenHovering()
    {
        var controlSystem = new Core.ControlSystem(ASleigh().On().Hovering());

        controlSystem.Descend();

        _output.ToString().Trim().Should().Be("The sleigh must be flying to descend.");
    }

    [Fact]
    public void TestPark()
    {
        var controlSystem = new Core.ControlSystem(ASleigh().Off());
        controlSystem.StartSystem();

        //we want to drain all the magic power to test the parking
        SafeAscendManyTimes(controlSystem, 10);

        controlSystem.Park();
        controlSystem.Ascend();

        _output.ToString()
            .Trim()
            .Should()
            .Be(
                """
                Starting the sleigh...
                System ready.
                Ascending...
                Ascending...
                Ascending...
                Ascending...
                Ascending...
                The reindeer needs rest. Please park the sleigh...
                The reindeer needs rest. Please park the sleigh...
                The reindeer needs rest. Please park the sleigh...
                The reindeer needs rest. Please park the sleigh...
                The reindeer needs rest. Please park the sleigh...
                Parking...
                Ascending...
                """);
    }

    [Fact]
    public void TestStop()
    {
        var controlSystem = new Core.ControlSystem(ASleigh().On());

        controlSystem.StopSystem();

        _output.ToString()
            .Trim()
            .Should()
            .Be(
                """
                Stopping the sleigh...
                System shutdown.
                """);
    }

    [Fact]
    public void TestAlreadyStop()
    {
        var controlSystem = new Core.ControlSystem(ASleigh().Off());

        controlSystem.StopSystem();

        _output.ToString()
            .Trim()
            .Should()
            .Be(
                """
                Stopping the sleigh...
                Cannot turn off the sleigh because it is already off.
                """);
    }

    private static void SafeAscendManyTimes(Core.ControlSystem controlSystem, int numberOfTimes)
    {
        for (var i = 0; i < numberOfTimes; i++)
        {
            controlSystem.Ascend();
        }
    }
}