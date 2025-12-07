using Day07.CI.Dependencies;

namespace Day07.CI;

public class Pipeline(IConfig config, IEmailer emailer, ILogger log)
{
    public void Run(Project project)
    {
        var input = PipelineResult.From(project, config.SendEmailSummary());


        var result = InternalRun(input);

        foreach (var (level, message) in result.GetLogs())
        {
            switch (level)
            {
                case LogLevel.Info:
                    log.Info(message);
                    break;
                case LogLevel.Error:
                    log.Error(message);
                    break;
            }
        }

        var potentialEmailMessage = result.GetPotentialEmailMessage();
        if (potentialEmailMessage is not null) emailer.Send(potentialEmailMessage);
    }

    private static PipelineResult InternalRun(PipelineResult input)
    {
        var steps = new List<IPipelineStep>
        {
            new TestStep(),
            new DeploymentStep(),
            new SendEmailSummaryStep()
        };

        foreach (var step in steps)
        {
            input.AddStepResult(step.Run(input));
        }

        return input;
    }
}

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

internal interface IPipelineStep
{
    internal IPipelineStepResult Run(PipelineResult input);
}

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

internal class SendEmailSummaryStep : IPipelineStep
{
    public IPipelineStepResult Run(PipelineResult input)
    {
        var result = SendSummaryPipelineStepResult.New();
        if (!input.ShouldSendEmailSummary())
            return result.AddLog(LogLevel.Info, "Email disabled");

        result = result.AddLog(LogLevel.Info, "Sending email");
        if (!input.IsTestsPassed())
            return result.SendEmail("Tests failed");

        if (!input.IsDeploymentSuccessful())
            return result.SendEmail("Deployment failed");

        return result.SendEmail("Deployment completed successfully");
    }
}