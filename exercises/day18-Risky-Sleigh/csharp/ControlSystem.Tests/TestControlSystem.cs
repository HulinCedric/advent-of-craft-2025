using FluentAssertions;
using static ControlSystem.Tests.SleighBuilder;

namespace ControlSystem.Tests;

public class TestControlSystem
{
    private readonly Core.ControlSystem _controlSystem;
    private readonly SpyDashboard _dashboard;

    public TestControlSystem()
    {
        _dashboard = new SpyDashboard();
        _controlSystem = new Core.ControlSystem(
            ASleigh().Off().Parked().Build(),
            _dashboard);
    }

    [Fact]
    public void TestStart()
    {
        _controlSystem.StartSystem();

        _dashboard.Output()
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
        _controlSystem.StartSystem();

        _controlSystem.StartSystem();

        _dashboard.Output()
            .Should()
            .Be(
                """
                Starting the sleigh...
                System ready.
                Starting the sleigh...
                Cannot turn on the sleigh because it is already on.
                """);
    }

    [Fact]
    public void TestAscend()
    {
        _controlSystem.StartSystem();

        _controlSystem.Ascend();

        _dashboard.Output()
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
        _controlSystem.StartSystem();
        _controlSystem.Ascend();
        _controlSystem.Descend();

        _dashboard.Output()
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
        _controlSystem.StartSystem();

        _controlSystem.Descend();

        _dashboard.Output()
            .Should()
            .Be(
                """
                Starting the sleigh...
                System ready.
                The sleigh must be flying to descend.
                """);
    }

    [Fact]
    public void TestDescendWhenHovering()
    {
        _controlSystem.StartSystem();
        _controlSystem.Ascend();
        _controlSystem.Descend();

        _controlSystem.Descend();

        _dashboard.Output()
            .Should()
            .Be(
                """
                Starting the sleigh...
                System ready.
                Ascending...
                Descending...
                The sleigh must be flying to descend.
                """);
    }

    [Fact]
    public void TestPark()
    {
        _controlSystem.StartSystem();

        _controlSystem.Ascend();
        _controlSystem.Ascend();
        _controlSystem.Ascend();
        _controlSystem.Ascend();
        _controlSystem.Ascend();
        _controlSystem.Ascend();

        _controlSystem.Park();
        _controlSystem.Ascend();

        _dashboard.Output()
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
                Parking...
                Ascending...
                """);
    }

    [Fact]
    public void TestStop()
    {
        _controlSystem.StartSystem();
        _controlSystem.StopSystem();

        _dashboard.Output()
            .Should()
            .Be(
                """
                Starting the sleigh...
                System ready.
                Stopping the sleigh...
                System shutdown.
                """);
    }

    [Fact]
    public void TestAlreadyStop()
    {
        _controlSystem.StopSystem();

        _dashboard.Output()
            .Should()
            .Be(
                """
                Stopping the sleigh...
                Cannot turn off the sleigh because it is already off.
                """);
    }
}