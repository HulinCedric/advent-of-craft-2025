using Day07.CI.Dependencies;

namespace Day07.CI;

internal class DeploymentStep(Project project) : IPipelineStep
{
    public IPipelineStepResult Handle(FunctionalCorePipeline input)
    {
        if (!input.StepsResults.IsTestsPassed())
            return DeploymentStepResult.StepFailed();

        if (project.Deploy() != "success")
            return DeploymentStepResult.StepFailed().AddLog(LogLevel.Error, "Deployment failed");

        return DeploymentStepResult.StepPassed().AddLog(LogLevel.Info, "Deployment successful");
    }
}