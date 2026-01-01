using ControlSystem.Core;
using FluentAssertions;
using FluentAssertions.LanguageExt;
using static ControlSystem.Core.SleighAction;
using static ControlSystem.Core.SleighEngineStatus;
using static ControlSystem.Tests.SleighBuilder;

namespace ControlSystem.Tests;

public class SleighShould
{
    [Fact]
    public void Be_Off_by_default() => Sleigh.New().Status.Should().Be(Off);

    [Fact]
    public void Be_Parked_by_default() => Sleigh.New().Action.Should().Be(Parked);

    [Fact]
    public void Ascend_changes_action_to_Flying()
        => ASleigh()
            .On()
            .Build()
            .Ascend(41)
            .Should()
            .BeRight()
            .Which.Action.Should()
            .Be(Flying);

    [Fact]
    public void Descend_changes_action_to_Hovering()
        => ASleigh()
            .On()
            .Flying()
            .Build()
            .Descend()
            .Should()
            .BeRight()
            .Which.Action.Should()
            .Be(Hovering);

    [Fact]
    public void Park_changes_action_to_Parked()
        => ASleigh()
            .On()
            .Build()
            .Park()
            .Should()
            .BeRight()
            .Which.Action.Should()
            .Be(Parked);
}