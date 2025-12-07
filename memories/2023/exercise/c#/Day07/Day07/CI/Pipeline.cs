using Day07.CI.Dependencies;
using LanguageExt;

namespace Day07.CI;

public class Pipeline(IConfig config, IEmailer emailer, ILogger log)
{
    public void Run(Project project)
    {
        var result = FunctionalCorePipeline.Run(project, config.SendEmailSummary());

        Logs(result);
        SendEmail(result);
    }

    private void Logs(Seq<IPipelineStepResult> result)
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

    private void SendEmail(Seq<IPipelineStepResult> result) => result.GetSummaryEmailMessage().Do(emailer.Send);
}