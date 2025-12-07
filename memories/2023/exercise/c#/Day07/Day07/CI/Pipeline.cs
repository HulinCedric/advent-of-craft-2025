using Day07.CI.Dependencies;

namespace Day07.CI;

public class Pipeline(IConfig config, IEmailer emailer, ILogger log)
{
    public void Run(Project project)
    {
        var input = PipelineResult.Empty(project);
        var result = InternalCore(input);

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

        if (result.EmailMessage is not null)
        {
            emailer.Send(result.EmailMessage);
        }
    }

    private PipelineResult InternalCore(PipelineResult input)
    {
        bool deploySuccessful;

        if (input.Project.HasTests())
        {
            if (input.Project.RunTests() == "success")
            {
                input.Info("Tests passed");
                input.TestsPass();
            }
            else
            {
                input.Error("Tests failed");
                input.TestsFail();
            }
        }
        else
        {
            input.Info("No tests");
            input.TestsPass();
        }

        if (input.IsTestsPassed())
        {
            if (input.Project.Deploy() == "success")
            {
                input.Info("Deployment successful");
                input.DeploymentSuccessful();
            }
            else
            {
                input.Error("Deployment failed");
                input.DeploymentFailed();
            }
        }
        else
        {
            input.DeploymentFailed();
        }

        

        return SendEmailSummary(input);
    }

    private PipelineResult SendEmailSummary(PipelineResult input)
    {
        if (config.SendEmailSummary())
        {
            input.Info("Sending email");
            if (input.IsTestsPassed())
            {
                if (input.IsDeploymentSuccessful())
                    input.Send("Deployment completed successfully");
                else
                    input.Send("Deployment failed");
            }
            else
            {
                input.Send("Tests failed");
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
    private bool _isTestsPassed;
    private readonly List<(LogLevel, string)> _logs;
    private string? _emailMessage;
    private bool _isDeploymentSuccessful;

    private PipelineResult(Project project, List<(LogLevel, string)> logs, string? emailMessage)
    {
        Project = project;
        _logs = logs;
        _emailMessage = emailMessage;
        _isTestsPassed = false;
        _isDeploymentSuccessful = false;
    }

    public Project Project { get; }
    public IReadOnlyList<(LogLevel, string)> Logs => _logs;
    public string? EmailMessage => _emailMessage;
    public bool IsTestsPassed() => _isTestsPassed;
    public bool IsDeploymentSuccessful() => _isDeploymentSuccessful;

    public static PipelineResult Empty(Project project) => new(project, new List<(LogLevel, string)>(), null);

    public void Info(string message) => _logs.Add((LogLevel.Info, message));

    public void Error(string message) => _logs.Add((LogLevel.Error, message));

    public void Send(string message) => _emailMessage = message;

    public void TestsPass()
    {
        _isTestsPassed = true;
    }

    public void TestsFail()
    {
        _isTestsPassed = false;
    }

    public void DeploymentSuccessful()
    {
        _isDeploymentSuccessful = true;
    }

    public void DeploymentFailed()
    {
        _isDeploymentSuccessful = false;
    }
}

internal enum LogLevel
{
    Info,
    Error
}