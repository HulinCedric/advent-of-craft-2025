namespace Day07.CI;

internal interface IPipelineStep
{
    internal PipelineResult Run(PipelineResult input);
}