namespace Day07.CI;

internal class DeploymentStep : IPipelineStep
{
    public IPipelineStepResult Run(PipelineResult input)
    {
        if (!input.IsTestsPassed())
            return DeploymentStepResult.StepFailed();

        if (input.Project.Deploy() != "success")
            return DeploymentStepResult.StepFailed().AddLog(LogLevel.Error, "Deployment failed");

        return DeploymentStepResult.StepPassed().AddLog(LogLevel.Info, "Deployment successful");
    }
}