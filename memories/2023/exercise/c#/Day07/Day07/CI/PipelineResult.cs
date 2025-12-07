using Day07.CI.Dependencies;

namespace Day07.CI;

internal class PipelineResult
{
    private readonly List<(LogLevel, string)> _logs;
    private readonly bool _shouldSendEmailSummary;
    private bool _isDeploymentSuccessful;
    private bool _isTestsPassed;
    private string? _emailMessage;

    private PipelineResult(
        Project project,
        List<(LogLevel, string)> logs,
        string? emailMessage,
        bool shouldSendEmailSummary)
    {
        Project = project;
        _logs = logs;
        _emailMessage = emailMessage;
        _shouldSendEmailSummary = shouldSendEmailSummary;
        _isTestsPassed = false;
        _isDeploymentSuccessful = false;
    }

    public Project Project { get; }
    public IReadOnlyList<(LogLevel, string)> Logs => _logs;
    public string? GetPotentialEmailMessage() => _emailMessage;
    public bool IsTestsPassed() => _isTestsPassed;
    public bool IsDeploymentSuccessful() => _isDeploymentSuccessful;

    public static PipelineResult From(Project project, bool shouldSendEmailSummary)
        => new(project, [], null, shouldSendEmailSummary);

    public void LogInfo(string message) => _logs.Add((LogLevel.Info, message));

    public void LogError(string message) => _logs.Add((LogLevel.Error, message));

    public void SendEmail(string message) => _emailMessage = message;

    public void TestsPass() => _isTestsPassed = true;

    public void TestsFail() => _isTestsPassed = false;

    public void DeploymentSuccessful() => _isDeploymentSuccessful = true;

    public void DeploymentFailed() => _isDeploymentSuccessful = false;

    public bool ShouldSendEmailSummary() => _shouldSendEmailSummary;
}