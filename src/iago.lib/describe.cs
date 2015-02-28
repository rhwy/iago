namespace Iago
{
  using System;
  using System.Collections.Generic;
  using Microsoft.Framework.Logging;

  using static System.Console;

  public delegate string Specify();
  public delegate string Description();
  public delegate void DefineAction();
  public delegate void CheckAction();
  public delegate void CheckActionWithSamples(dynamic values);
  public class Serie
  {
    public static Serie Samples(params object[] items)
    {
      return new Serie();
    }
  }
  public static class Specs {
    private static ILogger logger;
    public static Func<ILogger> SetLogger = ()=> {
        throw new NotImplementedException(); return null;};

    public static void When(string definition, DefineAction act) {
      WriteLine("\t[when] "+definition);
      act();
    }

    public static void Then(string definition, CheckAction assert) {
      WriteLine("\t[then] "+definition);
      assert();
    }
    public static void Then(string definition,
      CheckActionWithSamples assert, params object[] values) {
      WriteLine("\t[then] "+definition);
      assert(values);
    }



    public static void And(string definition, Action assert){
      WriteLine("\t[.And] "+definition);
      assert();
    }
  }
}
