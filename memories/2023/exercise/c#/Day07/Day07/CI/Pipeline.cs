using Day07.CI.Dependencies;

namespace Day07.CI;

public class Pipeline(IConfig config, IEmailer emailer, ILogger log)
{
    public void Run(Project project)
    {
        var input = PipelineResult
            .From(project, config.SendEmailSummary());

        var result = Run(input);


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

    private static PipelineResult Run(PipelineResult input)
    {
        List<IPipelineStep> steps =
        [
            new TestStep(),
            new DeploymentStep(),
            new SendEmailSummaryStep()
        ];

        return steps.Aggregate(input, (current, step) => step.Handle(current));
    }
}