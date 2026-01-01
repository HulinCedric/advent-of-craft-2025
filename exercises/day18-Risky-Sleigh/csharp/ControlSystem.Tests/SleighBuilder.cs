using ControlSystem.Core;

namespace ControlSystem.Tests;

public class SleighBuilder
{
    private SleighAction _action = SleighAction.Parked;
    private SleighEngineStatus _status = SleighEngineStatus.Off;

    public static SleighBuilder ASleigh() => new();

    public SleighBuilder On()
    {
        _status = SleighEngineStatus.On;
        return this;
    }

    public SleighBuilder Off()
    {
        _status = SleighEngineStatus.Off;
        return this;
    }

    public SleighBuilder WithAction(SleighAction action)
    {
        _action = action;
        return this;
    }

    public SleighBuilder Parked()
    {
        _action = SleighAction.Parked;
        return this;
    }

    public SleighBuilder Hovering()
    {
        _action = SleighAction.Hovering;
        return this;
    }

    public SleighBuilder Flying()
    {
        _action = SleighAction.Flying;
        return this;
    }

    public Sleigh Build() => new(_status, _action);

    public static implicit operator Sleigh(SleighBuilder builder) => builder.Build();
}