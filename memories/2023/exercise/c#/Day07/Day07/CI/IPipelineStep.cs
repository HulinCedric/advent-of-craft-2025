namespace Day07.CI;

internal interface IPipelineStep
{
    internal PipelineResult Handle(PipelineResult input);
}