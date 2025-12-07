namespace Day07.CI;

internal class SendEmailSummaryStep : IPipelineStep
{
    public PipelineResult Handle(PipelineResult input) => input.AddStepResult(PipelineResult(input));

    private static IPipelineStepResult PipelineResult(PipelineResult input)
    {
        if (!input.ShouldSendEmailSummary())
            return SendSummaryPipelineStepResult.New()
                .AddLog(LogLevel.Info, "Email disabled");

        if (!input.IsTestsPassed())
            return SendSummaryPipelineStepResult.New()
                .AddLog(LogLevel.Info, "Sending email")
                .SendEmail("Tests failed");

        if (!input.IsDeploymentSuccessful())
            return SendSummaryPipelineStepResult.New()
                .AddLog(LogLevel.Info, "Sending email")
                .SendEmail("Deployment failed");

        return SendSummaryPipelineStepResult.New()
            .AddLog(LogLevel.Info, "Sending email")
            .SendEmail("Deployment completed successfully");
    }
}