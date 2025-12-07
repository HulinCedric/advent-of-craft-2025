using LanguageExt;

namespace Day07.CI;

internal sealed record SendSummaryPipelineStepResult : IPipelineStepResult
{
    private readonly IReadOnlyList<(LogLevel, string)> _logs;

    private SendSummaryPipelineStepResult(
        IReadOnlyList<(LogLevel, string)> logs,
        Option<string> summaryEmailMessage)
    {
        _logs = logs;
        SummaryEmailMessage = summaryEmailMessage;
    }

    public Option<string> SummaryEmailMessage { get; }
    public IReadOnlyList<(LogLevel, string)> GetLogs() => _logs;

    public static SendSummaryPipelineStepResult New() => new([], Option<string>.None);

    public SendSummaryPipelineStepResult AddLog(LogLevel level, string message)
        => new(new List<(LogLevel, string)>(_logs) { (level, message) }, SummaryEmailMessage);

    public SendSummaryPipelineStepResult SendEmail(string emailMessage) => new(_logs, emailMessage);
}