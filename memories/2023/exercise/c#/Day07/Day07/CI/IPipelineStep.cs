namespace Day07.CI;

internal interface IPipelineStep
{
    internal IPipelineStepResult Run(PipelineResult input);
}