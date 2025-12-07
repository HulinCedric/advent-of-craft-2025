namespace Day07.CI;

internal class TestStep : IPipelineStep
{
    public PipelineResult Run(PipelineResult input) => input.AddStepResult(RunInternal(input));

    private static IPipelineStepResult RunInternal(PipelineResult input)
    {
        if (!input.Project.HasTests())
            return TestStepResult.StepPassed().AddLog(LogLevel.Info, "No tests");

        if (input.Project.RunTests() != "success")
            return TestStepResult.StepFailed().AddLog(LogLevel.Error, "Tests failed");

        return TestStepResult.StepPassed().AddLog(LogLevel.Info, "Tests passed");
    }
}