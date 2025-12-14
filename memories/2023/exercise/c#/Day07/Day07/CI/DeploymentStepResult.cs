using LanguageExt;

namespace Day07.CI;

internal sealed record DeploymentStepResult : IPipelineStepResult
{
    private readonly Seq<(LogLevel, string)> _logs;

    private DeploymentStepResult(bool isPassed, Seq<(LogLevel, string)> logs)
    {
        IsPassed = isPassed;
        _logs = logs;
    }

    public bool IsPassed { get; }

    public Seq<(LogLevel, string)> GetLogs() => _logs;

    public static DeploymentStepResult StepPassed() => new(true, []);

    public static DeploymentStepResult StepFailed() => new(false, []);

    public DeploymentStepResult AddLog(LogLevel level, string message) => new(IsPassed, _logs.Add((level, message)));
}