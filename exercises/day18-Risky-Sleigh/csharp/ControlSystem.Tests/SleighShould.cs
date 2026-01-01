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
            .Ascend()
            .Should()
            .BeRight()
            .Which.Action.Should()
            .Be(Flying);

    [Theory]
    [InlineData(Parked)]
    [InlineData(Hovering)]
    public void Not_descend_when(SleighAction initialAction)
        => ASleigh()
            .On()
            .WithAction(initialAction)
            .Build()
            .Descend()
            .Should()
            .BeLeft()
            .Which.Should()
            .Be("The sleigh must be flying to descend.");
    
    [Fact]
    public void Descend_when_on_and_flying()
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