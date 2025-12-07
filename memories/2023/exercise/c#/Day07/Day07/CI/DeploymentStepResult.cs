namespace Day07.CI;

internal sealed record DeploymentStepResult : IPipelineStepResult
{
    private readonly IReadOnlyList<(LogLevel, string)> _logs;

    private DeploymentStepResult(bool IsPassed, IReadOnlyList<(LogLevel, string)> logs)
    {
        this.IsPassed = IsPassed;
        _logs = logs;
    }

    public bool IsPassed { get; }

    public IReadOnlyList<(LogLevel, string)> GetLogs() => _logs;

    public static DeploymentStepResult StepPassed() => new(true, []);

    public static DeploymentStepResult StepFailed() => new(false, []);

    public DeploymentStepResult AddLog(LogLevel level, string message)
        => new(IsPassed, new List<(LogLevel, string)>(_logs) { (level, message) });
}