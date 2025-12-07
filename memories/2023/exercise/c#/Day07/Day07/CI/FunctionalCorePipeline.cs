using Day07.CI.Dependencies;
using LanguageExt;

namespace Day07.CI;

public sealed record FunctionalCorePipeline
{
    private FunctionalCorePipeline(IEnumerable<IPipelineStepResult> pipelineStepResults)
        => StepsResults = pipelineStepResults.ToSeq();

    public Seq<IPipelineStepResult> StepsResults { get; }

    public static FunctionalCorePipeline Run(Project project, bool sendEmailSummary)
        => Run(
            new TestStep(project),
            new DeploymentStep(project),
            new SendEmailSummaryStep(sendEmailSummary));

    private static FunctionalCorePipeline Run(params IEnumerable<IPipelineStep> steps)
        => steps.Aggregate(New(), (current, step) => current.AddStepResult(step.Handle(current)));

    private static FunctionalCorePipeline New() => new([]);

    private FunctionalCorePipeline AddStepResult(IPipelineStepResult stepResult)
        => new(StepsResults.Append(stepResult));
}