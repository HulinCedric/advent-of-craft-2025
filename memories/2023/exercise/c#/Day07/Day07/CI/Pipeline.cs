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

        if (result.EmailMessage is not null) emailer.Send(result.EmailMessage);
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

internal class PipelineResult
{
    private readonly List<(LogLevel, string)> _logs;
    private readonly bool _shouldSendEmailSummary;
    private bool _isDeploymentSuccessful;
    private bool _isTestsPassed;
    private string? _emailMessage;

    private PipelineResult(
        Project project,
        List<(LogLevel, string)> logs,
        string? emailMessage,
        bool shouldSendEmailSummary)
    {
        Project = project;
        _logs = logs;
        _emailMessage = emailMessage;
        _shouldSendEmailSummary = shouldSendEmailSummary;
        _isTestsPassed = false;
        _isDeploymentSuccessful = false;
    }

    public Project Project { get; }
    public IReadOnlyList<(LogLevel, string)> Logs => _logs;
    public string? EmailMessage => _emailMessage;

    public bool IsTestsPassed() => _isTestsPassed;
    public bool IsDeploymentSuccessful() => _isDeploymentSuccessful;

    public static PipelineResult From(Project project, bool shouldSendEmailSummary)
        => new(project, [], null, shouldSendEmailSummary);

    public void LogInfo(string message) => _logs.Add((LogLevel.Info, message));

    public void LogError(string message) => _logs.Add((LogLevel.Error, message));

    public void SendEmail(string message) => _emailMessage = message;

    public void TestsPass() => _isTestsPassed = true;

    public void TestsFail() => _isTestsPassed = false;

    public void DeploymentSuccessful() => _isDeploymentSuccessful = true;

    public void DeploymentFailed() => _isDeploymentSuccessful = false;

    public bool ShouldSendEmailSummary() => _shouldSendEmailSummary;
}

internal enum LogLevel
{
    Info,
    Error
}