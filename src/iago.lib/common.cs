namespace Iago {
  using System;

  public interface Result<T>
  {
    T Value {get;}
    bool AsExpected {get;}
    Exception Error {get;}

  }

  public class ApplicationTime
  {
      private static Func<DateTime> timeNow = ()=> DateTime.Now;

      public static void SetTimeProvider(Func<DateTime> time)
      {
          timeNow = time;
      }

      public static DateTime Now
      {
          get
          {
              return timeNow();
          }
      }

      private static DateTime start = timeNow();

      public static TimeSpan StopTime
      {
          get
          {
              return (timeNow() - start);
          }
      }

      private static Func<TimeSpan,string> timerFormat = (s) =>
      {

          return $"[{s.Seconds:00}:{s.Milliseconds:000}]";
      };

      public static void SetDefaultFormat(Func<TimeSpan,string> format)
      {
          timerFormat = format;
      }

      public static string FormatTimer(TimeSpan span)
      {
          return timerFormat(span);
      }
  }

  public class Success<T> : Result<T>
  {
      public T Value {get;}
      public bool AsExpected {get;} = true;
      public Exception Error {get;} = null;
      public Success(T value)
      {
        Value = value;
      }
  }
  public class Failure<T> : Result<T>
  {
      public T Value {get;}
      public bool AsExpected {get;} = false;
      public Exception Error {get;} = null;
      public Failure(Exception error)
      {
        Error = error;
      }
  }
  public static class ResultExtensions
  {
    public static Result<U> Then<T,U>(
      this Result<T> result, Func<T,U> success, Func<Exception,Exception> error)
      {
        if(result.AsExpected)
        {
          return new Success<U>(success(result.Value));
        }
        return new Failure<U>(error(result.Error));
      }

      public static Result<U> Then<T,U>(
        this Result<T> result, Func<T,U> success)
        {
          if(result.AsExpected)
          {
            return new Success<U>(success(result.Value));
          }
          return new Failure<U>(result.Error);
        }
  }
  public struct Unit {}

  public class Option<T>
  {
    public T Value {get;}
    public Option(T value)
    {
      Value = value;
    }
  }
}
