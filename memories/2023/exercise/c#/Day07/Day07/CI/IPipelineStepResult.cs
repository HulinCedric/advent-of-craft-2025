namespace Day07.CI;

internal interface IPipelineStepResult
{
    IReadOnlyList<(LogLevel, string)> GetLogs();
}