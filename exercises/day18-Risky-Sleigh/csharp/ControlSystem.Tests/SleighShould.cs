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
        => ASleigh().On().Build()
            .Ascend(41)
            .Should()
            .BeRight()
            .Which.Action.Should()
            .Be(Flying);
}