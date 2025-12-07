using Day07.CI.Dependencies;

namespace Day07.CI
{
    public class Pipeline(IConfig config, IEmailer emailer, ILogger log)
    {
        public void Run(Project project)
        {
            var logs = InternalCore(project);
            
            foreach (var (level, message) in logs)
            {
                switch (level)
                {
                    case LogLevel.Info:
                        log.Info(message);
                        break;
                    case LogLevel.Error:
                        log.Error(message);
                        break;
                }
            }
        }

        private List<(LogLevel, string)> InternalCore(Project project)
        {
            bool testsPassed;
            bool deploySuccessful;

            var logs = new List<(LogLevel, string)>();

            if (project.HasTests())
            {
                if (project.RunTests() == "success")
                {
                    logs.Add((LogLevel.Info, "Tests passed"));
                    testsPassed = true;
                }
                else
                {
                    logs.Add((LogLevel.Error, "Tests failed"));
                    testsPassed = false;
                }
            }
            else
            {
                logs.Add((LogLevel.Error, "Tests failed"));
                testsPassed = true;
            }

            if (testsPassed)
            {
                if (project.Deploy() == "success")
                {
                    logs.Add((LogLevel.Info, "Deployment successful"));
                    deploySuccessful = true;
                }
                else
                {
                    logs.Add((LogLevel.Error, "Deployment failed"));
                    deploySuccessful = false;
                }
            }
            else
            {
                deploySuccessful = false;
            }

            if (config.SendEmailSummary())
            {
                logs.Add((LogLevel.Info, "Sending email"));
                if (testsPassed)
                {
                    if (deploySuccessful)
                    {
                        emailer.Send("Deployment completed successfully");
                    }
                    else
                    {
                        emailer.Send("Deployment failed");
                    }
                }
                else
                {
                    emailer.Send("Tests failed");
                }
            }
            else
            {
                // log.Info("Email disabled");
                logs.Add((LogLevel.Info, "Email disabled"));
            }

            return logs;
        }
    }

    internal enum LogLevel
    {
        Info,
        Error
    }
}