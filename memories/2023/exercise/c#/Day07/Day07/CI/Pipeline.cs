using Day07.CI.Dependencies;

namespace Day07.CI;

public class Pipeline(IConfig config, IEmailer emailer, ILogger log)
{
    public void Run(Project project)
    {
        var input = PipelineResult.From(project, config.SendEmailSummary());

        var result = InternalRun(input);

        foreach (var (level, message) in result.Logs)
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
        if (!input.IsTestsPassed())
            return input.StepFailed(Steps.Deployment);

        if (input.Project.Deploy() != "success")
            return input.StepFailed(Steps.Deployment, "Deployment failed");

        return input.StepPassed(Steps.Deployment, "Deployment successful");
    }

    private static PipelineResult SendEmailSummary(PipelineResult input)
    {
        if (!input.ShouldSendEmailSummary())
        {
            input.LogInfo("Email disabled");
            return input;
        }

        input.LogInfo("Sending email");
        if (!input.IsTestsPassed())
        {
            input.SendEmail("Tests failed");
            return input;
        }

        if (!input.IsDeploymentSuccessful())
        {
            input.SendEmail("Deployment failed");
            return input;
        }

        input.SendEmail("Deployment completed successfully");
        return input;
    }
}

internal class TestStep
{
    internal PipelineStepResult Run(PipelineResult input)
    {
        if (!input.Project.HasTests())
            return new PipelineStepResult(Steps.Test, IsPassed: true, "No tests");

        if (input.Project.RunTests() != "success")
            return new PipelineStepResult(Steps.Test, IsPassed: false, "Tests failed");

        return new PipelineStepResult(Steps.Test, IsPassed: true, "Tests passed");
    }
}
