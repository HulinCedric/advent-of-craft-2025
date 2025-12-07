using LanguageExt;

namespace Day07.CI;

internal sealed record FunctionalCorePipeline
{
    private readonly Seq<IPipelineStepResult> _stepsResults;

    private FunctionalCorePipeline(IEnumerable<IPipelineStepResult> pipelineStepResults)
        => _stepsResults = pipelineStepResults.ToSeq();


    public IReadOnlyList<(LogLevel, string)> GetLogs()
        => _stepsResults
            .SelectMany(stepResult => stepResult.GetLogs())
            .ToList();

    public string? GetPotentialEmailMessage()
        => _stepsResults.OfType<SendSummaryPipelineStepResult>().FirstOrDefault()?.EmailMessage;

    public bool IsTestsPassed() => _stepsResults.OfType<TestStepResult>().FirstOrDefault()?.IsPassed ?? false;

    public bool IsDeploymentSuccessful()
        => _stepsResults.OfType<DeploymentStepResult>().FirstOrDefault()?.IsPassed ?? false;

    private static FunctionalCorePipeline New() => new([]);

    public FunctionalCorePipeline AddStepResult(IPipelineStepResult pipelineStepResult)
        => new(_stepsResults.Append(pipelineStepResult));

    public static FunctionalCorePipeline Run(params IEnumerable<IPipelineStep> steps)
        => steps.Aggregate(New(), (current, step) => step.Handle(current));
}