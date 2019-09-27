using System;
using System.Collections.Generic;
using Iago.Common;

namespace Iago.Tests
{
    public class TestLogger : ILogger
    {
        public Stack<string> LogLines = new Stack<string>();
        private string formatLevel(LogLevel level) => $"[{level.ToString()}]";
        public void LogInformation(string message) => Log(LogLevel.Information, state: message);
        public void LogVerbose(string message) => Log(LogLevel.Verbose, state: message);

        public void LogError(string message) => Log(LogLevel.Error, state: message);

        public void LogWarning(string message) => Log(LogLevel.Warning, state: message);
        public void Log(LogLevel logLevel, int eventId = 0, object state = null, Exception exception = null, Func<object, Exception, string> formatter = null)
        {
            LogLines.Push(formatLevel(logLevel) + $" {state}");
        }

        public IDisposable BeginScope(object scope)
        {
            throw new MaybeNotNeededException();
        }
    }
}