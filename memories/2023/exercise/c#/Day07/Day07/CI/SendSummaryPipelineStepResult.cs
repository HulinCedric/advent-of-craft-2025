namespace Day07.CI;

internal sealed record SendSummaryPipelineStepResult : IPipelineStepResult
{
    private readonly IReadOnlyList<(LogLevel, string)> _logs;

    private SendSummaryPipelineStepResult(
        string Name,
        bool IsPassed,
        IReadOnlyList<(LogLevel, string)> logs,
        string? emailMessage = null)
    {
        _logs = logs;
        this.Name = Name;
        this.IsPassed = IsPassed;
        EmailMessage = emailMessage;
    }

    public string? EmailMessage { get; }
    public string Name { get; }
    public bool IsPassed { get; }

    public IReadOnlyList<(LogLevel, string)> GetLogs() => _logs;

    public SendSummaryPipelineStepResult AddLog(LogLevel level, string message)
        => new(Name, IsPassed, new List<(LogLevel, string)>(_logs) { (level, message) }, EmailMessage);

    public static SendSummaryPipelineStepResult New() => new("SendEmailSummary", true, []);

    public SendSummaryPipelineStepResult SendEmail(string emailMessage) => new(Name, IsPassed, _logs, emailMessage);
}