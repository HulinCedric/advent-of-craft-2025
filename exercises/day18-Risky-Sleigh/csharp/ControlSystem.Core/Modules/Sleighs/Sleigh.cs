using LanguageExt;

namespace ControlSystem.Core.Modules.Sleighs;

public record Sleigh(SleighEngineStatus Status, SleighAction Action)
{
    private const string SleighNotStartedFailure =
        "The sleigh is not started. Please start the sleigh before any other action...";

    public static Sleigh New() => new(SleighEngineStatus.Off, SleighAction.Parked);

    public Either<string, Sleigh> TurnOn()
    {
        if (Status == SleighEngineStatus.On)
            return "The sleigh is already started.";

        return this with { Status = SleighEngineStatus.On };
    }

    public Either<string, Sleigh> TurnOff()
    {
        if (Status == SleighEngineStatus.Off)
            return "The sleigh is already stopped.";

        return this with { Status = SleighEngineStatus.Off };
    }

    public Either<string, Sleigh> Ascend()
    {
        if (Status != SleighEngineStatus.On) return SleighNotStartedFailure;

        return this with { Action = SleighAction.Flying };
    }

    public Either<string, Sleigh> Descend()
    {
        if (Status != SleighEngineStatus.On) return SleighNotStartedFailure;

        if (Action != SleighAction.Flying)
            return "The sleigh must be flying to descend. You can try to ascend or park the sleigh...";

        return this with { Action = SleighAction.Hovering };
    }

    public Either<string, Sleigh> Park()
    {
        if (Status != SleighEngineStatus.On) return SleighNotStartedFailure;

        return this with { Action = SleighAction.Parked };
    }
}