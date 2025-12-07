namespace Day07.CI;

internal interface IPipelineStep
{
    internal FunctionalCorePipeline Handle(FunctionalCorePipeline input);
}