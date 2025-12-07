using LanguageExt;

namespace Day07.CI;

internal static class StepResultsExtensions
{
    extension(Seq<IPipelineStepResult> stepResults)
    {
        internal Seq<(LogLevel, string)> GetLogs()
            => stepResults
                .SelectMany(stepResult => stepResult.GetLogs())
                .ToSeq();

        internal Option<string> GetSummaryEmailMessage()
            => stepResults
                .OfType<SendSummaryPipelineStepResult>()
                .Bind(r => r.SummaryEmailMessage)
                .HeadOrNone();

        internal bool IsTestsPassed()
            => stepResults
                .OfType<TestStepResult>()
                .Map(stepResult => stepResult.IsPassed)
                .HeadOrNone()
                .IfNone(false);

        internal bool IsDeploymentSuccessful()
            => stepResults
                .OfType<DeploymentStepResult>()
                .Map(stepResult => stepResult.IsPassed)
                .HeadOrNone()
                .IfNone(false);
    }
}