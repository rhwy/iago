namespace Iago.Abstractions
{
	using System;
	
    public interface ILogger
	{
		void LogInformation(string message);
		void LogVerbose(string message);
		void LogError(string message);
		void LogWarning(string message);
		void Log(
          LogLevel logLevel, int eventId = 0, object state = null,
          Exception exception = null, Func<object, Exception, string> formatter = null);
		  
	    IDisposable BeginScope(object scope);
	}
	
	public enum LogLevel {
		Information, Warning, Error, Verbose
	}
}