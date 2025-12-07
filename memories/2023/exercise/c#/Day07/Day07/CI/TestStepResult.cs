using LanguageExt;

namespace Day07.CI;

internal sealed record TestStepResult : IPipelineStepResult
{
    private readonly Seq<(LogLevel, string)> _logs;

    private TestStepResult(bool isPassed, Seq<(LogLevel, string)> logs)
    {
        IsPassed = isPassed;
        _logs = logs;
    }

    public bool IsPassed { get; }

    public Seq<(LogLevel, string)> GetLogs() => _logs;

    public static TestStepResult StepPassed() => new(true, []);

    public static TestStepResult StepFailed() => new(false, []);

    public TestStepResult AddLog(LogLevel level, string message) => new(IsPassed, _logs.Add((level, message)));
}