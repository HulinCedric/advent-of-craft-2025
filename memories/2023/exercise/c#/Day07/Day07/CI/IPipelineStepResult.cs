namespace Day07.CI;

internal interface IPipelineStepResult
{
    bool IsPassed { get; }
    IReadOnlyList<(LogLevel, string)> GetLogs();
}