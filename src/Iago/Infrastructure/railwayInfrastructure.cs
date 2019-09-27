using System;

namespace Iago.Common {
  public interface Result<T>
  {
    T Value {get;}
    bool AsExpected {get;}
    Exception Error {get;}

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
    public static Option<T> Nothing {get;}= new Option<T>();
    public static Option<T> Of(T value)
    {
        if(value == null)
        {
            return Nothing;
        }
        return new Option<T>(value);
    }
    private Option()
    {
        Value = default(T);
    }
    private Option(T value)
    {
      Value = value;
    }
  }
}
