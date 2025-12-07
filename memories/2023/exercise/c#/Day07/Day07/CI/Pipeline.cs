using Day07.CI.Dependencies;

namespace Day07.CI;

public class Pipeline(IConfig config, IEmailer emailer, ILogger log)
{
    public void Run(Project project)
    {
        var pipelineResult = new PipelineResult(project, new List<(LogLevel, string)>());
        var result = InternalCore(pipelineResult);

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
    }

    private PipelineResult InternalCore(PipelineResult input)
    {
        bool testsPassed;
        bool deploySuccessful;

        if (input.Project.HasTests())
        {
            if (input.Project.RunTests() == "success")
            {
                input.Info("Tests passed");
                testsPassed = true;
            }
            else
            {
                input.Error("Tests failed");
                testsPassed = false;
            }
        }
        else
        {
            input.Info("No tests");
            testsPassed = true;
        }

        if (testsPassed)
        {
            if (input.Project.Deploy() == "success")
            {
                input.Info("Deployment successful");
                deploySuccessful = true;
            }
            else
            {
                input.Error("Deployment failed");
                deploySuccessful = false;
            }
        }
        else
        {
            deploySuccessful = false;
        }

        if (config.SendEmailSummary())
        {
            log.Info("Sending email");
            if (testsPassed)
            {
                if (deploySuccessful)
                    emailer.Send("Deployment completed successfully");
                else
                    emailer.Send("Deployment failed");
            }
            else
            {
                emailer.Send("Tests failed");
            }
        }
        else
        {
            input.Info("Email disabled");
        }

        return input;
    }
}

internal class PipelineResult
{
    private readonly List<(LogLevel, string)> _logs;

    public PipelineResult(Project project, IEnumerable<(LogLevel, string)> logs)
    {
        Project = project;
        Logs = logs.ToList();
    }

    public Project Project { get; }
    public IReadOnlyList<(LogLevel, string)> Logs { get; }


    public void Info(string message) => _logs.Add((LogLevel.Info, message));

    public void Error(string message) => _logs.Add((LogLevel.Error, message));
}

internal enum LogLevel
{
    Info,
    Error
}