namespace Iago
{
  using System;
  using System.Dynamic;

  using System.Collections.Generic;
  using Microsoft.Framework.Logging;

  using static System.Console;

  public delegate string Specify();
  public delegate string Description();
  public delegate void DefineAction();
  public delegate void CheckAction();
  //public delegate void CheckActionWithSamples(dynamic values);
  public delegate void CheckActionWithSamples<T>(T values);

  public class Serie
  {
    private object[] source;
    public Serie(object[] source)
    {
      this.source = source;
    }
    public static Serie Samples(object[] samples)
    {
      return new Serie(samples);
    }
  }

  public static class Specs {
    private static ILogger logger;
    private static Func<ILogger> setLogger = ()=> {
        throw new NotImplementedException(); return null;};

    public static void SetLogger(Func<ILogger> logDefinition)
    {
      setLogger = logDefinition;
      logger = setLogger();
    }

    public static void When(string definition, DefineAction act) {
      logger.WriteInformation("    [when] "+definition);
      act();
    }

    public static void Then(string definition, CheckAction assert) {
      logger.WriteInformation("      [then] "+definition);
      assert();
    }
    public static void Then<T>(string definition,
      CheckActionWithSamples<T> assert, T values) {
        logger.WriteInformation("      [then] "+definition);
        assert(values);
    }

    public static void Then<T>(string definition,
      CheckActionWithSamples<T> assert, IEnumerable<T> values) {

        logger.WriteInformation("      [then] "+definition);
        int testCounter=0;
        foreach(T value in values)
        {
          try
          {
            testCounter++;
            assert(value);
          }
          catch(Exception ex)
          {
            throw new SampleSeriesException(testCounter,ex);
          }

        }

    }



    public static void And(string definition, Action assert){
      logger.WriteInformation("      [.And] "+definition);
      assert();
    }
  }

  [System.Serializable]
  public class SampleSeriesException : Exception
  {
      public static string GetMessageFromNumber(int number, Exception inner=null)
      {
          return $"Sample #{number} :"+
          (inner!=null?inner.Message:String.Empty);
      }

      public SampleSeriesException() { }
      public SampleSeriesException(int sampleNumber) : base(GetMessageFromNumber(sampleNumber)) { }
      public SampleSeriesException(int sampleNumber, Exception inner) : base(GetMessageFromNumber(sampleNumber,inner), inner) { }
      protected SampleSeriesException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context)
      { }
      public override string ToString()
      {
          if(base.InnerException==null)
              return base.ToString();
          return string.Format(
              $"{Message}{InnerException}");
      }
  }

  public class DynamicSample : DynamicObject
  {
    object sourceValues;
    public DynamicSample(object args)
    {
      this.sourceValues = args;

    }
    public override bool TryGetMember(GetMemberBinder binder, out object result)
    {
      result = new object();

      return true;
    }

    public override string ToString()
    {
        return this.sourceValues?.ToString();
    }
  }
}
