namespace Day07.CI;

public interface IPipelineStepResult
{
    IReadOnlyList<(LogLevel, string)> GetLogs();
}