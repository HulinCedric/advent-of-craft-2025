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

        if (result.GetPotentialEmailMessage() is not null) emailer.Send(result.GetPotentialEmailMessage());
    }

    private static PipelineResult InternalRun(PipelineResult input)
    {
        input = RunTests(input);
        input = RunDeployment(input);
        return SendEmailSummary(input);
    }

    private static PipelineResult RunTests(PipelineResult input)
    {
        if (!input.Project.HasTests())
        {
            input.LogInfo("No tests");
            input.TestsPass();
            return input;
        }

        if (input.Project.RunTests() == "success")
        {
            input.LogInfo("Tests passed");
            input.TestsPass();
            return input;
        }

        input.LogError("Tests failed");
        input.TestsFail();
        return input;
    }

    private static PipelineResult RunDeployment(PipelineResult input)
    {
        if (!input.IsTestsPassed())
        {
            input.DeploymentFailed();
            return input;
        }

        if (input.Project.Deploy() == "success")
        {
            input.LogInfo("Deployment successful");
            input.DeploymentSuccessful();
            return input;
        }

        input.LogError("Deployment failed");
        input.DeploymentFailed();
        return input;
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

        if (input.IsDeploymentSuccessful())
        {
            input.SendEmail("Deployment completed successfully");
            return input;
        }

        input.SendEmail("Deployment failed");
        return input;
    }
}