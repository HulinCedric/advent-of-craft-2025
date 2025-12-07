using Day07.CI.Dependencies;

namespace Day07.CI
{
    public class Pipeline(IConfig config, IEmailer emailer, ILogger log)
    {
        public void Run(Project project)
        {
            var result = InternalCore(project);

            foreach (var (level, message) in result.Logs)
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

        private PipelineResult InternalCore(Project project)
        {
            bool testsPassed;
            bool deploySuccessful;

            var result = new PipelineResult(new List<(LogLevel, string)>());

            if (project.HasTests())
            {
                if (project.RunTests() == "success")
                {
                    result.Info("Tests passed");
                    testsPassed = true;
                }
                else
                {
                    result.Error("Tests failed");
                    testsPassed = false;
                }
            }
            else
            {
                result.Info("No tests");
                testsPassed = true;
            }

            if (testsPassed)
            {
                if (project.Deploy() == "success")
                {
                    result.Info("Deployment successful");
                    deploySuccessful = true;
                }
                else
                {
                    result.Error("Deployment failed");
                    deploySuccessful = false;
                }
            }
            else
            {
                deploySuccessful = false;
            }

            if (config.SendEmailSummary())
            {
                log.Info("Sending email");
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
                result.Info("Email disabled");
            }

            return result;
        }
    }

    internal class PipelineResult
    {
        private readonly List<(LogLevel, string)> _logs;
        public IReadOnlyList<(LogLevel, string)> Logs { get; }

        public PipelineResult(IEnumerable<(LogLevel, string)> logs)
        {
            Logs = logs.ToList();
        }


        public void Info(string message)
        {
            _logs.Add((LogLevel.Info, message));
        }

        public void Error(string message)
        {
            _logs.Add((LogLevel.Error, message));
        }
    }

    internal enum LogLevel
    {
        Info,
        Error
    }
}