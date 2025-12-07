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
        input = RunTests(input);
        input = RunDeployment(input);
        return SendEmailSummary(input);
    }

    private static PipelineResult RunTests(PipelineResult input)
    {
        var pipelineStepResult = new TestStep().Run(input);
        return input.AddStepResult(pipelineStepResult);
    }

    private static PipelineResult RunDeployment(PipelineResult input)
    {
        var pipelineStepResult = new DeploymentStep().Run(input);
        return input.AddStepResult(pipelineStepResult);
    }

    private static PipelineResult SendEmailSummary(PipelineResult input)
    {
        var pipelineStepResult = new SendEmailSummaryStep().Run(input);
        return input.AddStepResult(pipelineStepResult);
    }
}

internal class TestStep
{
    internal PipelineStepResult Run(PipelineResult input)
    {
        if (!input.Project.HasTests())
            return PipelineStepResult.StepPassed(Steps.Test, "No tests");

        if (input.Project.RunTests() != "success")
            return PipelineStepResult.StepFailed(Steps.Test, "Tests failed");

        return PipelineStepResult.StepPassed(Steps.Test, "Tests passed");
    }
}

internal class DeploymentStep
{
    internal PipelineStepResult Run(PipelineResult input)
    {
        if (!input.IsTestsPassed())
            return PipelineStepResult.StepFailed(Steps.Deployment);

        if (input.Project.Deploy() != "success")
            return PipelineStepResult.StepFailed(Steps.Deployment, "Deployment failed");

        return PipelineStepResult.StepPassed(Steps.Deployment, "Deployment successful");
    }
}

internal class SendEmailSummaryStep
{
    internal IPipelineStepResult Run(PipelineResult input)
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