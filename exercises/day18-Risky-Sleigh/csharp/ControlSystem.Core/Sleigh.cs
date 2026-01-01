namespace ControlSystem.Core;

public record Sleigh(SleighEngineStatus Status, SleighAction Action)
{
    public SleighEngineStatus Status { get; set; } = Status;
    public SleighAction Action { get; set; } = Action;

    public static Sleigh New() => new(SleighEngineStatus.Off, SleighAction.Parked);
}