namespace Day07.CI;

internal class TestStep : IPipelineStep
{
    public IPipelineStepResult Run(PipelineResult input)
    {
        if (!input.Project.HasTests())
            return PipelineStepResult.StepPassed(Steps.Test, "No tests");

        if (input.Project.RunTests() != "success")
            return PipelineStepResult.StepFailed(Steps.Test, "Tests failed");

        return PipelineStepResult.StepPassed(Steps.Test, "Tests passed");
    }
}