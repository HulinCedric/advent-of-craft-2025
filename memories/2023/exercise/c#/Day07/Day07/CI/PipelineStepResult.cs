namespace Day07.CI;

internal sealed record PipelineStepResult(string Name, bool IsPassed, string Message = "");