namespace Day07.CI;

internal class SendEmailSummaryStep : IPipelineStep
{
    public FunctionalCorePipeline Handle(FunctionalCorePipeline input) => input.AddStepResult(PipelineResult(input));

    private static IPipelineStepResult PipelineResult(FunctionalCorePipeline input)
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