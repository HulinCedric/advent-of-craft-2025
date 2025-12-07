using Day07.CI.Dependencies;

namespace Day07.CI;

internal class PipelineResult
{
    private readonly bool _shouldSendEmailSummary;
    private readonly IReadOnlyList<IPipelineStepResult> _stepsResults;

    private PipelineResult(
        Project project,
        bool shouldSendEmailSummary,
        IEnumerable<IPipelineStepResult> pipelineStepResults)
    {
        Project = project;
        _shouldSendEmailSummary = shouldSendEmailSummary;
        _stepsResults = pipelineStepResults.ToList().AsReadOnly();
    }

    public Project Project { get; }

    public IReadOnlyList<(LogLevel, string)> GetLogs()
        => _stepsResults
            .SelectMany(stepResult => stepResult.GetLogs())
            .ToList();

    public string? GetPotentialEmailMessage()
        => _stepsResults.OfType<SendSummaryPipelineStepResult>().FirstOrDefault()?.EmailMessage;

    public bool IsTestsPassed() => _stepsResults.OfType<TestStepResult>().FirstOrDefault()?.IsPassed ?? false;

    public bool IsDeploymentSuccessful()
        => _stepsResults.OfType<DeploymentStepResult>().FirstOrDefault()?.IsPassed ?? false;

    public static PipelineResult From(Project project, bool shouldSendEmailSummary)
        => new(project, shouldSendEmailSummary, []);

    public bool ShouldSendEmailSummary() => _shouldSendEmailSummary;

    public PipelineResult AddStepResult(IPipelineStepResult pipelineStepResult)
        => new(
            Project,
            _shouldSendEmailSummary,
            _stepsResults.Append(pipelineStepResult));

    public PipelineResult Run(params IEnumerable<IPipelineStep> steps)
        => steps.Aggregate(this, (current, step) => step.Handle(current));
}