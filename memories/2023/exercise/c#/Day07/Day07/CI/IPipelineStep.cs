namespace Day07.CI;

internal interface IPipelineStep
{
    internal IPipelineStepResult Handle(FunctionalCorePipeline input);
}