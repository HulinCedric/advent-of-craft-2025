using Day07.CI.Dependencies;

namespace Day07.CI;

internal class PipelineResult
{
    private readonly List<(LogLevel, string)> _logs;
    private readonly bool _shouldSendEmailSummary;
    private readonly List<PipelineStepResult> _stepsResults;
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
        _stepsResults = [];
    }

    public Project Project { get; }
    public IReadOnlyList<(LogLevel, string)> Logs => _logs;
    public string? GetPotentialEmailMessage() => _emailMessage;
    public bool IsTestsPassed() => _stepsResults.FirstOrDefault(s => s.Name == Steps.Test)?.IsPassed ?? false;

    public bool IsDeploymentSuccessful()
        => _stepsResults.FirstOrDefault(s => s.Name == Steps.Deployment)?.IsPassed ?? false;

    public static PipelineResult From(Project project, bool shouldSendEmailSummary)
        => new(project, [], null, shouldSendEmailSummary);

    public void LogInfo(string message) => _logs.Add((LogLevel.Info, message));

    public void LogError(string message) => _logs.Add((LogLevel.Error, message));

    public void SendEmail(string message) => _emailMessage = message;

    public bool ShouldSendEmailSummary() => _shouldSendEmailSummary;

    public PipelineResult StepPassed(string stepName, string message)
    {
        _stepsResults.Add(new PipelineStepResult(stepName, IsPassed: true));

        LogInfo(message);

        return this;
    }

    public PipelineResult StepFailed(string stepName, string message)
    {
        _stepsResults.Add(new PipelineStepResult(stepName, IsPassed: false));

        LogError(message);

        return this;
    }

    public PipelineResult StepFailed(string stepName)
    {
        _stepsResults.Add(new PipelineStepResult(stepName, IsPassed: false));

        return this;
    }

    public PipelineResult AddStepResult(PipelineStepResult pipelineStepResult)
    {
        _stepsResults.Add(pipelineStepResult);

        if (string.IsNullOrEmpty(pipelineStepResult.Message)) return this;

        if (pipelineStepResult.IsPassed)
            LogInfo(pipelineStepResult.Message);
        else
            LogError(pipelineStepResult.Message);

        return this;
    }
}