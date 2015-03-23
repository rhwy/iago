using System;
using System.Linq;
using Microsoft.Framework.Logging;

using static System.Console;

namespace Iago
{
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
      this.logger.WriteInformation($"end {scope} ".PadRight(60,'-'));

    }
  }

  public class SimpleConsoleLogger : ILogger
  {
      private int currentScopeDepth;
      public int CurrentScopeDepth
      {
          get{ return currentScopeDepth;}
      }

      private string WriteTab () {
        return string.Join("",Enumerable.Range(1,CurrentScopeDepth).Select(x=>"\t"));
      }

      public void DisposeScope()
      {
        if(currentScopeDepth > 0)
        {
          currentScopeDepth--;
        }
      }
      public void Write(
        LogLevel logLevel, int eventId = 0, object state = null,
        Exception exception = null, Func<object, Exception, string> formatter = null)
      {
        if(logLevel == LogLevel.Information)
        {
          Console.ForegroundColor = ConsoleColor.Green;
          Console.Write("[PASS] ");
          Console.ResetColor();
          Console.Write(WriteTab());
          Console.WriteLine(state);
          return;
        }
        if(logLevel == LogLevel.Warning)
        {
          Console.ForegroundColor = ConsoleColor.Yellow;
          Console.Write("[INFO] ");
          Console.Write(WriteTab());
          Console.ResetColor();
          Console.WriteLine(state);
          return;
        }
        if(logLevel == LogLevel.Error)
        {
          Console.ForegroundColor = ConsoleColor.Red;
          Console.Write("[FAIL] ");
          Console.Write(WriteTab());
          Console.WriteLine(state);
          Console.ResetColor();
          return;
        }

      }
      public IDisposable BeginScope(object scope)
      {

        this.WriteInformation($"{scope} ".PadRight(60,'-'));
        currentScopeDepth++;
        return new SimpleLoggerScope(this,scope);
      }

      public bool IsEnabled(LogLevel logLevel)
      {
        return LoggerActivation(logLevel);
      }

      public static Func<LogLevel,bool> LoggerActivation
      = (level) => true;
  }
}
