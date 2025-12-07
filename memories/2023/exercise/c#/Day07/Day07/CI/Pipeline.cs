using Day07.CI.Dependencies;

namespace Day07.CI;

public class Pipeline(IConfig config, IEmailer emailer, ILogger log)
{
    public void Run(Project project)
    {
        var input = PipelineResult.Empty(project, config.SendEmailSummary());
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

        if (result.EmailMessage is not null) emailer.Send(result.EmailMessage);
    }

    private static PipelineResult InternalRun(PipelineResult input)
    {
        input = RunTests(input);
        input = RunDeployment(input);
        return SendEmailSummary(input);
    }

    private static PipelineResult RunDeployment(PipelineResult input)
    {
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

        return input;
    }

    private static PipelineResult RunTests(PipelineResult input)
    {
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

        return input;
    }

    private static PipelineResult SendEmailSummary(PipelineResult input)
    {
        if (input.SendEmailSummary())
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
    private readonly List<(LogLevel, string)> _logs;
    private readonly bool _shouldSendEmailSummary;
    private bool _isDeploymentSuccessful;
    private bool _isTestsPassed;

    private PipelineResult(
        Project project,
        List<(LogLevel, string)> logs,
        string? emailMessage,
        bool shouldSendEmailSummary)
    {
        Project = project;
        _logs = logs;
        EmailMessage = emailMessage;
        _shouldSendEmailSummary = shouldSendEmailSummary;
        _isTestsPassed = false;
        _isDeploymentSuccessful = false;
    }

    public Project Project { get; }
    public IReadOnlyList<(LogLevel, string)> Logs => _logs;
    public string? EmailMessage { get; private set; }

    public bool IsTestsPassed() => _isTestsPassed;
    public bool IsDeploymentSuccessful() => _isDeploymentSuccessful;

    public static PipelineResult Empty(Project project, bool shouldSendEmailSummary)
        => new(project, [], null, shouldSendEmailSummary);

    public void Info(string message) => _logs.Add((LogLevel.Info, message));

    public void Error(string message) => _logs.Add((LogLevel.Error, message));

    public void Send(string message) => EmailMessage = message;

    public void TestsPass() => _isTestsPassed = true;

    public void TestsFail() => _isTestsPassed = false;

    public void DeploymentSuccessful() => _isDeploymentSuccessful = true;

    public void DeploymentFailed() => _isDeploymentSuccessful = false;

    public bool SendEmailSummary() => _shouldSendEmailSummary;
}

internal enum LogLevel
{
    Info,
    Error
}