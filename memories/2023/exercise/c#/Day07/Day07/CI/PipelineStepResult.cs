namespace Day07.CI;

internal sealed record PipelineStepResult(string Name, bool IsPassed, string Message = "")
{
    public static PipelineStepResult StepPassed(string name, string message = "")
    {
        return new PipelineStepResult(name, true, message);
    }

    public static PipelineStepResult StepFailed(string name, string message = "")
    {
        return new PipelineStepResult(name, false, message);
    }
}