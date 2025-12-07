using Day07.CI.Dependencies;
using LanguageExt;

namespace Day07.CI;

internal sealed record FunctionalCorePipeline
{
    private readonly Seq<IPipelineStepResult> _stepsResults;

    private FunctionalCorePipeline(
        Project project,
        IEnumerable<IPipelineStepResult> pipelineStepResults)
    {
        Project = project;
        _stepsResults = pipelineStepResults.ToSeq();
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

    public static FunctionalCorePipeline From(Project project) => new(project, []);


    public FunctionalCorePipeline AddStepResult(IPipelineStepResult pipelineStepResult)
        => new(
            Project,
            _stepsResults.Append(pipelineStepResult));

    public FunctionalCorePipeline Run(params IEnumerable<IPipelineStep> steps)
        => steps.Aggregate(this, (current, step) => step.Handle(current));
}