namespace Iago
{
    using System;
    using System.Linq;
    
    using Iago.Abstractions;
    
    public class SimpleLoggerScope : IDisposable
    {
        private SimpleConsoleLogger logger;
        private object scope;
        public SimpleLoggerScope(SimpleConsoleLogger logger, object scope)
        {
            this.logger = logger;
            this.scope = scope;
        }
        public void Dispose()
        {
            this.logger.DisposeScope();
            this.logger.LogInformation($"end {scope} ".PadRight(60, ' '));

        }
    }

    public class SimpleConsoleLogger : ILogger
    {
        private int currentScopeDepth;
        public int CurrentScopeDepth
        {
            get { return currentScopeDepth; }
        }

        
        private string WriteTab()
        {
            return string.Join("", Enumerable.Range(1, CurrentScopeDepth).Select(x => " "));
        }

        public void DisposeScope()
        {
            if (currentScopeDepth > 0)
            {
                currentScopeDepth--;
            }
        }
        public void Log(
          LogLevel logLevel, int eventId = 0, object state = null,
          Exception exception = null, Func<object, Exception, string> formatter = null)
        {
            var timestamp = ApplicationTime.FormatTimer(ApplicationTime.StopTime);
            var lines = state?.ToString()
                .Split(Environment.NewLine.ToCharArray())
                .Where(x => !string.IsNullOrEmpty(x));
            if (lines.Count() > 1)
            {
                lines.ToList().ForEach(x => Log(logLevel, eventId, x));
                return;
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(timestamp);
            Console.ResetColor();



            if (logLevel == LogLevel.Information)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(" ✔ ");
                Console.ResetColor();
                Console.Write(WriteTab());
                Console.WriteLine(state);
                return;
            }
            if (logLevel == LogLevel.Warning)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" ● ");
                Console.Write(WriteTab());
                Console.WriteLine(state);
                Console.ResetColor();

                return;
            }
            if (logLevel == LogLevel.Verbose)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(" ○ ");
                Console.Write(WriteTab());
                Console.WriteLine(state);
                Console.ResetColor();

                return;
            }
            if (logLevel == LogLevel.Error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" ✘ ");
                Console.Write(WriteTab());
                Console.WriteLine(state);
                Console.ResetColor();
                return;
            }

        }
        
        public void LogInformation(string message)
        {
            Log(LogLevel.Information, Guid.NewGuid().GetHashCode(), message);
        }
        public void LogWarning(string message)
        {
            Log(LogLevel.Warning, Guid.NewGuid().GetHashCode(), message);
        }
        public void LogError(string message)
        {
            Log(LogLevel.Error, Guid.NewGuid().GetHashCode(), message);
        }
        public void LogVerbose(string message)
        {
            Log(LogLevel.Verbose, Guid.NewGuid().GetHashCode(), message);
        }
        public IDisposable BeginScope(object scope)
        {

            this.LogInformation($"{scope} ".PadRight(60, ' '));
            currentScopeDepth++;
            return new SimpleLoggerScope(this, scope);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return LoggerActivation(logLevel);
        }

        public static Func<LogLevel, bool> LoggerActivation
        = (level) => true;
    }
}
