using Day07.CI.Dependencies;

namespace Day07.CI;

internal class TestStep(Project project) : IPipelineStep
{
    public IPipelineStepResult Handle(FunctionalCorePipeline input)
    {
        if (!project.HasTests())
            return TestStepResult.StepPassed().AddLog(LogLevel.Info, "No tests");

        if (project.RunTests() != "success")
            return TestStepResult.StepFailed().AddLog(LogLevel.Error, "Tests failed");

        return TestStepResult.StepPassed().AddLog(LogLevel.Info, "Tests passed");
    }
}