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
        => new(project, shouldSendEmailSummary);

    public bool ShouldSendEmailSummary() => _shouldSendEmailSummary;

    public PipelineResult AddStepResult(IPipelineStepResult pipelineStepResult)
    {
        _stepsResults.Add(pipelineStepResult);
        return this;
    }

    public PipelineResult InternalRun()
    {
        var steps = new List<IPipelineStep>
        {
            new TestStep(),
            new DeploymentStep(),
            new SendEmailSummaryStep()
        };

        return steps.Aggregate(this, (current, step) => step.Handle(current));
    }
}