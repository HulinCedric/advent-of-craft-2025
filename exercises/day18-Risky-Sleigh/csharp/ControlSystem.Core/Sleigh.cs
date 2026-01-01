using LanguageExt;

namespace ControlSystem.Core;

public record Sleigh(SleighEngineStatus Status, SleighAction Action)
{
    private const int RequiredMagicPowerForAscend = 40;

    private const string SleighNotStartedFailure =
        "The sleigh is not started. Please start the sleigh before any other action...";

    public static Sleigh New() => new(SleighEngineStatus.Off, SleighAction.Parked);

    public Either<string, Sleigh> TurnOn()
    {
        if (Status == SleighEngineStatus.On)
            return "Cannot turn on the sleigh because it is already on.";

        return this with { Status = SleighEngineStatus.On };
    }

    public Either<string, Sleigh> TurnOff()
    {
        if (Status == SleighEngineStatus.Off)
            return "Cannot turn off the sleigh because it is already off.";

        return this with { Status = SleighEngineStatus.Off };
    }

    public Either<string, Sleigh> Ascend(float availableMagicPower)
    {
        if (Status != SleighEngineStatus.On) return SleighNotStartedFailure;

        if (availableMagicPower < RequiredMagicPowerForAscend)
            return "The reindeer needs rest. Please park the sleigh...";

        return this with { Action = SleighAction.Flying };
    }

    public Either<string, Sleigh> Descend()
    {
        if (Status != SleighEngineStatus.On) return SleighNotStartedFailure;

        if (Action != SleighAction.Flying) return "The sleigh must be flying to descend.";

        return this with { Action = SleighAction.Hovering };
    }

    public Either<string, Sleigh> Park()
    {
        if (Status != SleighEngineStatus.On) return SleighNotStartedFailure;

        return this with { Action = SleighAction.Parked };
    }
}