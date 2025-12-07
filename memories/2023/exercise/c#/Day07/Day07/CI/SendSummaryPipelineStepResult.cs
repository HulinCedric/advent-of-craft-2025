namespace Day07.CI;

internal sealed record SendSummaryPipelineStepResult : IPipelineStepResult
{
    private readonly IReadOnlyList<(LogLevel, string)> _logs;

    private SendSummaryPipelineStepResult(
        IReadOnlyList<(LogLevel, string)> logs,
        string? emailMessage = null)
    {
        _logs = logs;
        EmailMessage = emailMessage;
    }

    public string? EmailMessage { get; }
    public IReadOnlyList<(LogLevel, string)> GetLogs() => _logs;

    public static SendSummaryPipelineStepResult New() => new([]);

    public SendSummaryPipelineStepResult AddLog(LogLevel level, string message)
        => new(new List<(LogLevel, string)>(_logs) { (level, message) }, EmailMessage);

    public SendSummaryPipelineStepResult SendEmail(string emailMessage) => new(_logs, emailMessage);
}