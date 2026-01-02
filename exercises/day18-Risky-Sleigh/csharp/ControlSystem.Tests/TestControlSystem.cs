using ControlSystem.Core;
using ControlSystem.Core.Models.Reindeers;
using ControlSystem.Core.Models.Reindeers.Factories;
using ControlSystem.Core.Models.Reindeers.Ports;
using ControlSystem.Core.Ports;
using ControlSystem.Infrastructure;
using ControlSystem.Tests.TestDoubles;
using FluentAssertions;
using static ControlSystem.Tests.Builders.SleighBuilder;

namespace ControlSystem.Tests;

public class TestControlSystem
{
    private readonly Core.Services.ControlSystem _controlSystem;
    private readonly SpyDashboard _dashboard;

    public TestControlSystem()
    {
        var availableSpecialAmplifiers = new Dictionary<int, AmplifierType>
        {
            { 1, AmplifierType.Divine },
            { 2, AmplifierType.Blessed },
            { 3, AmplifierType.Blessed }
        };

        IPowerUnitFactory factory = new BestMagicalPerformancePowerUnitFactory(
            new ReindeerRepository().GetAllReindeers(),
            availableSpecialAmplifiers);

        _dashboard = new SpyDashboard();
        _controlSystem = new Core.Services.ControlSystem(
            ASleigh().Off().Parked().Build(),
            _dashboard,
            factory);
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
                The sleigh is already started.
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
                The sleigh must be flying to descend. You can try to ascend or park the sleigh...
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
                The sleigh must be flying to descend. You can try to ascend or park the sleigh...
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
                The sleigh is already stopped.
                """);
    }
}