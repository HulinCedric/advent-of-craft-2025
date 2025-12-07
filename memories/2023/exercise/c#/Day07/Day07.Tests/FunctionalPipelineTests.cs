using Day07.CI;
using Day07.CI.Dependencies;
using FluentAssertions;
using FluentAssertions.LanguageExt;
using Xunit;
using static Day07.CI.Dependencies.TestStatus;

namespace Day07.Tests;

public class FunctionalPipelineTests
{
    [Fact]
    public void Project_With_Tests_That_Deploys_Successfully_With_Email_Notification()
    {
        var sendEmailSummary = true;

        var project = Project.Builder()
            .With(PassingTests)
            .Deployed(true)
            .Build();

        var result = FunctionalCorePipeline.Run(project, sendEmailSummary: sendEmailSummary);

        result.GetLogs()
            .Should()
            .BeEquivalentTo(
            [
                (LogLevel.Info, "Tests passed"),
                (LogLevel.Info, "Deployment successful"),
                (LogLevel.Info, "Sending email")
            ]);

        result.GetSummaryEmailMessage().Should().Be("Deployment completed successfully");
    }

    [Fact]
    public void Project_Without_Tests_That_Deploys_Successfully_With_Email_Notification()
    {
        var sendEmailSummary = true;

        var project = Project.Builder()
            .With(NoTests)
            .Deployed(true)
            .Build();

        var result = FunctionalCorePipeline.Run(project, sendEmailSummary: sendEmailSummary);

        result.GetLogs()
            .Should()
            .BeEquivalentTo(
            [
                (LogLevel.Info, "No tests"),
                (LogLevel.Info, "Deployment successful"),
                (LogLevel.Info, "Sending email")
            ]);

        result.GetSummaryEmailMessage().Should().Be("Deployment completed successfully");
    }

    [Fact]
    public void Project_Without_Tests_That_Deploys_Successfully_Without_Email_Notification()
    {
        var sendEmailSummary = false;

        var project = Project.Builder()
            .With(NoTests)
            .Deployed(true)
            .Build();

        var result = FunctionalCorePipeline.Run(project, sendEmailSummary: sendEmailSummary);

        result.GetLogs()
            .Should()
            .BeEquivalentTo(
            [
                (LogLevel.Info, "No tests"),
                (LogLevel.Info, "Deployment successful"),
                (LogLevel.Info, "Email disabled")
            ]);

        result.GetSummaryEmailMessage().Should().BeNone();
    }

    [Fact]
    public void Project_With_Tests_That_Fail_With_Email_Notification()
    {
        var sendEmailSummary = true;

        var project = Project.Builder()
            .With(FailingTests)
            .Deployed(true)
            .Build();

        var result = FunctionalCorePipeline.Run(project, sendEmailSummary: sendEmailSummary);

        result.GetLogs()
            .Should()
            .BeEquivalentTo(
            [
                (LogLevel.Error, "Tests failed"),
                (LogLevel.Info, "Sending email")
            ]);

        result.GetSummaryEmailMessage().Should().Be("Tests failed");
    }

    [Fact]
    public void Project_With_Tests_That_Fail_Without_Email_Notification()
    {
        var sendEmailSummary = false;

        var project = Project.Builder()
            .With(FailingTests)
            .Build();

        var result = FunctionalCorePipeline.Run(project, sendEmailSummary);

        result.GetLogs()
            .Should()
            .BeEquivalentTo(
            [
                (LogLevel.Error, "Tests failed"),
                (LogLevel.Info, "Email disabled")
            ]);

        result.GetSummaryEmailMessage().Should().BeNone();
    }

    [Fact]
    public void Project_With_Tests_And_Failing_Build_With_Email_Notification()
    {
        var sendEmailSummary = true;

        var project = Project.Builder()
            .With(PassingTests)
            .Deployed(false)
            .Build();

        var result = FunctionalCorePipeline.Run(project, sendEmailSummary);

        result.GetLogs()
            .Should()
            .BeEquivalentTo(
            [
                (LogLevel.Info, "Tests passed"),
                (LogLevel.Error, "Deployment failed"),
                (LogLevel.Info, "Sending email")
            ]);

        result.GetSummaryEmailMessage().Should().Be("Deployment failed");
    }

    [Fact]
    public void Project_With_Tests_And_Failing_Build_Without_Email_Notification()
    {
        var sendEmailSummary = false;

        var project = Project.Builder()
            .With(PassingTests)
            .Deployed(false)
            .Build();

        var result = FunctionalCorePipeline.Run(project, sendEmailSummary);

        result.GetLogs()
            .Should()
            .BeEquivalentTo(
            [
                (LogLevel.Info, "Tests passed"),
                (LogLevel.Error, "Deployment failed"),
                (LogLevel.Info, "Email disabled")
            ]);

        result.GetSummaryEmailMessage().Should().BeNone();
    }

    [Fact]
    public void Project_Without_Tests_And_Failing_Build_With_Email_Notification()
    {
        var sendEmailSummary = true;

        var project = Project.Builder()
            .With(NoTests)
            .Deployed(false)
            .Build();

        var result = FunctionalCorePipeline.Run(project, sendEmailSummary);

        result.GetLogs()
            .Should()
            .BeEquivalentTo(
            [
                (LogLevel.Info, "No tests"),
                (LogLevel.Error, "Deployment failed"),
                (LogLevel.Info, "Sending email")
            ]);

        result.GetSummaryEmailMessage().Should().Be("Deployment failed");
    }

    [Fact]
    public void Project_Without_Tests_And_Failing_Build_Without_Email_Notification()
    {
        var sendEmailSummary = false;

        var project = Project.Builder()
            .With(NoTests)
            .Deployed(false)
            .Build();

        var result = FunctionalCorePipeline.Run(project, sendEmailSummary);

        result.GetLogs()
            .Should()
            .BeEquivalentTo(
            [
                (LogLevel.Info, "No tests"),
                (LogLevel.Error, "Deployment failed"),
                (LogLevel.Info, "Email disabled")
            ]);

        result.GetSummaryEmailMessage().Should().BeNone();
    }
}