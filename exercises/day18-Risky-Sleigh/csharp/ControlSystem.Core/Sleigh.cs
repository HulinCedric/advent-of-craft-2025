using LanguageExt;

namespace ControlSystem.Core;

public record Sleigh(SleighEngineStatus Status, SleighAction Action)
{
    private const int RequiredMagicPowerForAscend = 40;
    
    private const string SleighNotStartedFailure =
        "The sleigh is not started. Please start the sleigh before any other action...";

    public SleighEngineStatus Status { get; private set; } = Status;
    public SleighAction Action { get; private set; } = Action;

    public static Sleigh New() => new(SleighEngineStatus.Off, SleighAction.Parked);

    public Either<string, Unit> TurnOn()
    {
        if (Status == SleighEngineStatus.On)
            return "Cannot turn on the sleigh because it is already on.";

        Status = SleighEngineStatus.On;

        return Unit.Default;
    }

    public Either<string, Unit> TurnOff()
    {
        if (Status == SleighEngineStatus.Off)
            return "Cannot turn off the sleigh because it is already off.";

        Status = SleighEngineStatus.Off;

        return Unit.Default;
    }

    public Either<string, Unit> Ascend(float availableMagicPower)
    {
        if (Status != SleighEngineStatus.On) return SleighNotStartedFailure;

        if (availableMagicPower < RequiredMagicPowerForAscend)
        {
            return "The reindeer needs rest. Please park the sleigh...";
        }
        
        Action = SleighAction.Flying;

        return Unit.Default;
    }

    public Either<string, Unit> Descend()
    {
        if (Status != SleighEngineStatus.On) return SleighNotStartedFailure;

        if (Action != SleighAction.Flying) return "The sleigh must be flying to descend.";

        Action = SleighAction.Hovering;

        return Unit.Default;
    }

    public Either<string, Unit> Park()
    {
        if (Status != SleighEngineStatus.On) return SleighNotStartedFailure;
        
        Action = SleighAction.Parked;

        return Unit.Default;
    }
}