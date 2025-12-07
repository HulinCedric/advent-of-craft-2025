namespace Day07.CI;

internal sealed record TestStepResult : IPipelineStepResult
{
    private readonly IReadOnlyList<(LogLevel, string)> _logs;

    private TestStepResult(bool isPassed, IReadOnlyList<(LogLevel, string)> logs)
    {
        this.IsPassed = isPassed;
        _logs = logs;
    }

    public bool IsPassed { get; }

    public IReadOnlyList<(LogLevel, string)> GetLogs() => _logs;

    public static TestStepResult StepPassed() => new(true, []);

    public static TestStepResult StepFailed() => new(false, []);

    public TestStepResult AddLog(LogLevel level, string message)
        => new(IsPassed, [.._logs, (level, message)]);
}