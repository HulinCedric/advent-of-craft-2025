using LanguageExt;

namespace Day07.CI;

internal sealed record SendSummaryPipelineStepResult : IPipelineStepResult
{
    private readonly Seq<(LogLevel, string)> _logs;

    private SendSummaryPipelineStepResult(
        Seq<(LogLevel, string)> logs,
        Option<string> summaryEmailMessage)
    {
        _logs = logs;
        SummaryEmailMessage = summaryEmailMessage;
    }

    public Option<string> SummaryEmailMessage { get; }
    public Seq<(LogLevel, string)> GetLogs() => _logs;

    public static SendSummaryPipelineStepResult New() => new([], Option<string>.None);

    public SendSummaryPipelineStepResult AddLog(LogLevel level, string message)
        => new(_logs.Add((level, message)), SummaryEmailMessage);

    public SendSummaryPipelineStepResult SendEmail(string emailMessage) => new(_logs, emailMessage);
}