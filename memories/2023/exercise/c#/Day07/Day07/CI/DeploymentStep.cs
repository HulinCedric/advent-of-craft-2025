using Day07.CI.Dependencies;

namespace Day07.CI;

internal class DeploymentStep(Project project) : IPipelineStep
{
    public FunctionalCorePipeline Handle(FunctionalCorePipeline input) => input.AddStepResult(PipelineResult(input));

    private IPipelineStepResult PipelineResult(FunctionalCorePipeline input)
    {
        if (!input.IsTestsPassed())
            return DeploymentStepResult.StepFailed();

        if (project.Deploy() != "success")
            return DeploymentStepResult.StepFailed().AddLog(LogLevel.Error, "Deployment failed");

        return DeploymentStepResult.StepPassed().AddLog(LogLevel.Info, "Deployment successful");
    }
}