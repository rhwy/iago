namespace Iago {
  using System;

  public interface Result<T>
  {
    T Value {get;}
    bool AsExpected {get;}
    Result<U> Then<U>(Func<T,U> success, Func<Exception> error);
  }

  public struct Unit {}
}
