using Day07.CI.Dependencies;

namespace Day07.CI;

public class Pipeline(IConfig config, IEmailer emailer, ILogger log)
{
    public void Run(Project project)
    {
        var result = PipelineResult
            .From(project, config.SendEmailSummary())
            .Run(
                new TestStep(),
                new DeploymentStep(),
                new SendEmailSummaryStep());

        Logs(result);
        SendEmail(result);
    }

    private void Logs(PipelineResult result)
    {
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
    }

    private void SendEmail(PipelineResult result)
    {
        var potentialEmailMessage = result.GetPotentialEmailMessage();
        if (potentialEmailMessage is not null) emailer.Send(potentialEmailMessage);
    }
}