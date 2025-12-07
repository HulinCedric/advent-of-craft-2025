namespace Day07.CI;

internal sealed record PipelineStepResult
{
    private readonly IReadOnlyList<(LogLevel, string)> _logs;

    private PipelineStepResult(string Name, bool IsPassed, IReadOnlyList<(LogLevel, string)> logs)
    {
        _logs = logs;
        this.Name = Name;
        this.IsPassed = IsPassed;
    }

    public string Name { get; }
    public bool IsPassed { get; }

    public static PipelineStepResult StepPassed(string name, string message = "")
    {
        if (!string.IsNullOrEmpty(message))
            return new PipelineStepResult(name, true, [(LogLevel.Info, message)]);

        return new PipelineStepResult(name, true, []);
    }

    public static PipelineStepResult StepFailed(string name, string message = "")
    {
        if (!string.IsNullOrEmpty(message))
            return new PipelineStepResult(name, false, [(LogLevel.Error, message)]);

        return new PipelineStepResult(name, false, []);
    }

    public IReadOnlyList<(LogLevel, string)> GetLogs() => _logs;
}