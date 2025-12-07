namespace Day07.CI;

internal interface IPipelineStepResult
{
    string Name { get; }
    bool IsPassed { get; }
    IReadOnlyList<(LogLevel, string)> GetLogs();
}