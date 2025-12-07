using LanguageExt;

namespace Day07.CI;

internal sealed record FunctionalCorePipeline
{
    private FunctionalCorePipeline(IEnumerable<IPipelineStepResult> pipelineStepResults)
        => StepsResults = pipelineStepResults.ToSeq();

    public Seq<IPipelineStepResult> StepsResults { get; }

    private static FunctionalCorePipeline New() => new([]);

    public static FunctionalCorePipeline Run(params IEnumerable<IPipelineStep> steps)
        => steps.Aggregate(New(), (current, step) => current.AddStepResult(step.Handle(current)));

    private FunctionalCorePipeline AddStepResult(IPipelineStepResult stepResult)
        => new(StepsResults.Append(stepResult));
}