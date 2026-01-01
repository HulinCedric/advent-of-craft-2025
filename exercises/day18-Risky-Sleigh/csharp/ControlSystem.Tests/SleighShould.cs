using ControlSystem.Core;
using FluentAssertions;

namespace ControlSystem.Tests;

public class SleighShould
{
    [Fact]
    public void Be_Off_by_default() => Sleigh.New().Status.Should().Be(SleighEngineStatus.Off);

    [Fact]
    public void Be_Parked_by_default() => Sleigh.New().Action.Should().Be(SleighAction.Parked);
}