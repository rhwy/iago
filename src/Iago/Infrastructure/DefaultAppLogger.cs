using System;

namespace Iago.Common
{
    public class DefaultAppLogger : ILogger
    {
        private AppConsole consoleLogger;
        public DefaultAppLogger()
        {
            consoleLogger = new AppConsole();
        }
        public void LogInformation(string message) => Log(LogLevel.Information, state : message);
        public void LogVerbose(string message) => Log(LogLevel.Verbose, state : message);

        public void LogError(string message) => Log(LogLevel.Error, state : message);

        public void LogWarning(string message) => Log(LogLevel.Warning, state : message);

        public void Log(LogLevel logLevel, int eventId = 0, object state = null, Exception exception = null, Func<object, Exception, string> formatter = null)
        {
            consoleLogger.WriteLine(state);
        }

        public IDisposable BeginScope(object scope)
        {
            throw new MaybeNotNeededException("");
        }
    }
}