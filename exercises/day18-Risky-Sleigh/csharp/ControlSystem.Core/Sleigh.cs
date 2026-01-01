using LanguageExt;

namespace ControlSystem.Core;

public record Sleigh(SleighEngineStatus Status, SleighAction Action)
{
    public SleighEngineStatus Status { get; set; } = Status;
    public SleighAction Action { get; set; } = Action;

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
}