using Day07.CI.Dependencies;

namespace Day07.CI;

internal class PipelineResult
{
    private readonly bool _shouldSendEmailSummary;
    private readonly List<IPipelineStepResult> _stepsResults;

    private PipelineResult(
        Project project,
        bool shouldSendEmailSummary)
    {
        Project = project;
        _shouldSendEmailSummary = shouldSendEmailSummary;
        _stepsResults = [];
    }

    public Project Project { get; }

    public IReadOnlyList<(LogLevel, string)> Logs
        => _stepsResults
            .SelectMany(stepResult => stepResult.GetLogs())
            .ToList();

    public string? GetPotentialEmailMessage()
        => _stepsResults.OfType<SendSummaryPipelineStepResult>().FirstOrDefault()?.EmailMessage;

    public bool IsTestsPassed() => _stepsResults.FirstOrDefault(s => s.Name == Steps.Test)?.IsPassed ?? false;

    public bool IsDeploymentSuccessful()
        => _stepsResults.FirstOrDefault(s => s.Name == Steps.Deployment)?.IsPassed ?? false;

    public static PipelineResult From(Project project, bool shouldSendEmailSummary)
        => new(project, shouldSendEmailSummary);

    public bool ShouldSendEmailSummary() => _shouldSendEmailSummary;

    public PipelineResult AddStepResult(IPipelineStepResult pipelineStepResult)
    {
        _stepsResults.Add(pipelineStepResult);
        return this;
    }
}