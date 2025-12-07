using Day07.CI.Dependencies;

namespace Day07.CI;

internal class PipelineResult
{
    private readonly List<(LogLevel, string)> _logs;
    private readonly bool _shouldSendEmailSummary;
    private readonly List<IPipelineStepResult> _stepsResults;
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

    public string? GetPotentialEmailMessage()
        => _stepsResults.OfType<SendSummaryPipelineStepResult>().FirstOrDefault()?.EmailMessage;

    public bool IsTestsPassed() => _stepsResults.FirstOrDefault(s => s.Name == Steps.Test)?.IsPassed ?? false;

    public bool IsDeploymentSuccessful()
        => _stepsResults.FirstOrDefault(s => s.Name == Steps.Deployment)?.IsPassed ?? false;

    public static PipelineResult From(Project project, bool shouldSendEmailSummary)
        => new(project, [], null, shouldSendEmailSummary);

    public void LogInfo(string message) => _logs.Add((LogLevel.Info, message));

    public void LogError(string message) => _logs.Add((LogLevel.Error, message));

    public void SendEmail(string message) => _emailMessage = message;

    public bool ShouldSendEmailSummary() => _shouldSendEmailSummary;

    public PipelineResult AddStepResult(IPipelineStepResult pipelineStepResult)
    {
        _stepsResults.Add(pipelineStepResult);

        _logs.AddRange(pipelineStepResult.GetLogs());

        return this;
    }
}