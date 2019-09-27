using System.Collections.Generic;
using Iago.Common;
using Iago.Language;
using NFluent;

namespace Iago.Tests
{
    using System;
    using Xunit;
    using static Iago.Language.Specs;
    
    public partial class GivenLanguageKeywords
    {
        public class WhenUsing_DescribeMethod
        {
            private TestLogger testLogger;

            public WhenUsing_DescribeMethod()
            {
                this.testLogger = new TestLogger();
                Specs.SetLogger(()=>testLogger);
            }
            [Fact]
            public void it_should_execute_action()
            {
                var counter = 0;
                It(" should set counter", () =>
                {
                    counter = 42;
                });
                Check.That(counter).IsEqualTo(42);
                Check.That(testLogger.LogLines.Pop())
                    .IsEqualTo("[Information]  [it]  should set counter");
            }
        }
    }

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
