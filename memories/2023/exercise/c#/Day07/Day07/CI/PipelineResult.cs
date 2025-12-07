using Day07.CI.Dependencies;

namespace Day07.CI;

internal class PipelineResult
{
    private readonly List<(LogLevel, string)> _logs;
    private readonly bool _shouldSendEmailSummary;
    private readonly List<PipelineStep> _steps;
    private string? _emailMessage;
    private bool _isDeploymentSuccessful;

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
        _isDeploymentSuccessful = false;
        _steps = [];
    }

    public Project Project { get; }
    public IReadOnlyList<(LogLevel, string)> Logs => _logs;
    public string? GetPotentialEmailMessage() => _emailMessage;
    public bool IsTestsPassed() => _steps.FirstOrDefault(s => s.Name == Steps.Test)?.IsPassed ?? false;
    public bool IsDeploymentSuccessful() => _steps.FirstOrDefault(s => s.Name == Steps.Deployment)?.IsPassed ?? false;

    public static PipelineResult From(Project project, bool shouldSendEmailSummary)
        => new(project, [], null, shouldSendEmailSummary);

    public void LogInfo(string message) => _logs.Add((LogLevel.Info, message));

    public void LogError(string message) => _logs.Add((LogLevel.Error, message));

    public void SendEmail(string message) => _emailMessage = message;

    public bool ShouldSendEmailSummary() => _shouldSendEmailSummary;

    public PipelineResult StepPassed(string stepName, string message)
    {
        _steps.Add(new PipelineStep(stepName, IsPassed: true));

        LogInfo(message);

        return this;
    }

    public PipelineResult StepFailed(string stepName, string message)
    {
        _steps.Add(new PipelineStep(stepName, IsPassed: false));

        LogError(message);

        return this;
    }

    public PipelineResult StepFailed(string stepName)
    {
        _steps.Add(new PipelineStep(stepName, IsPassed: false));
        
        return this;
    }
}