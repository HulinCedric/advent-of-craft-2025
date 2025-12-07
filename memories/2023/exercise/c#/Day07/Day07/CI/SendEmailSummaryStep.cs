namespace Day07.CI;

internal class SendEmailSummaryStep(bool sendEmailSummary) : IPipelineStep
{
    public FunctionalCorePipeline Handle(FunctionalCorePipeline input) => input.AddStepResult(PipelineResult(input));

    private IPipelineStepResult PipelineResult(FunctionalCorePipeline input)
    {
        if (!sendEmailSummary)
            return SendSummaryPipelineStepResult.New()
                .AddLog(LogLevel.Info, "Email disabled");

        if (!input.StepsResults.IsTestsPassed())
            return SendSummaryPipelineStepResult.New()
                .AddLog(LogLevel.Info, "Sending email")
                .SendEmail("Tests failed");

        if (!input.StepsResults.IsDeploymentSuccessful())
            return SendSummaryPipelineStepResult.New()
                .AddLog(LogLevel.Info, "Sending email")
                .SendEmail("Deployment failed");

        return SendSummaryPipelineStepResult.New()
            .AddLog(LogLevel.Info, "Sending email")
            .SendEmail("Deployment completed successfully");
    }
}