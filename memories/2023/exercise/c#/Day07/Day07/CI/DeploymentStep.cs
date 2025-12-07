namespace Day07.CI;

internal class DeploymentStep : IPipelineStep
{
    public IPipelineStepResult Run(PipelineResult input)
    {
        if (!input.IsTestsPassed())
            return PipelineStepResult.StepFailed(Steps.Deployment);

        if (input.Project.Deploy() != "success")
            return PipelineStepResult.StepFailed(Steps.Deployment, "Deployment failed");

        return PipelineStepResult.StepPassed(Steps.Deployment, "Deployment successful");
    }
}