namespace Day07.CI;

internal class SendEmailSummaryStep : IPipelineStep
{
    public IPipelineStepResult Run(PipelineResult input)
    {
        var result = SendSummaryPipelineStepResult.New();
        if (!input.ShouldSendEmailSummary())
            return result.AddLog(LogLevel.Info, "Email disabled");

        result = result.AddLog(LogLevel.Info, "Sending email");
        if (!input.IsTestsPassed())
            return result.SendEmail("Tests failed");

        if (!input.IsDeploymentSuccessful())
            return result.SendEmail("Deployment failed");

        return result.SendEmail("Deployment completed successfully");
    }
}