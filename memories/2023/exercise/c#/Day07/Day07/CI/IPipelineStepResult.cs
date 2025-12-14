using LanguageExt;

namespace Day07.CI;

public interface IPipelineStepResult
{
    Seq<(LogLevel, string)> GetLogs();
}