using System;
using System.Collections.Generic;
using System.Linq;
using Iago.Common;

namespace Iago.Language
{
    public static class Specs {
        private static ILogger logger;
        private static Func<ILogger> setLogger = ()=> new DefaultAppLogger();
        private static ILogger GetLogger()
        {
            if (logger == default) logger = setLogger();
            return logger;
        }
        public static void SetLogger(Func<ILogger> logDefinition)
        {
            setLogger = logDefinition;
            logger = setLogger();
        }

        public static void When(string name, DefineAction act) {
            GetLogger().LogInformation($"   [when] {name}");
            act();
        }

        public static void Describe(string name, DefineAction act) {
            GetLogger().LogInformation("[describe] "+ name);
            act();
        }

        public static void It(string name, DefineAction act) {
            GetLogger().LogInformation(" [it] "+name);
            act();
        }

        public static void Sample(object input, object output, object context = null)
        {
            Func<object,string> writeLines = (i)=>
            {
                if(i==null) return string.Empty;
                var lines = i.ToString()
                    .Split(Environment.NewLine.ToCharArray())
                    .Where(x=>!string.IsNullOrEmpty(x));
                var betterLines = lines.Select(line=> "      " + line);
                var betterMessage = string.Join(Environment.NewLine,betterLines);
                return betterMessage;
            };

            GetLogger().LogVerbose(" sample");
            if(context != null)
            {
                logger.LogVerbose(" - context:" + Environment.NewLine + writeLines(context));
            }
            GetLogger().LogVerbose(" - input  : " + Environment.NewLine + writeLines(input));
            GetLogger().LogVerbose(" - output : " + Environment.NewLine + writeLines(output));

        }
        public static void Then(string definition, CheckAction assert) {
            GetLogger().LogInformation("    [then] "+definition);
            assert();
        }
        public static void Then<T>(string definition,
            CheckActionWithSamples<T> assert, T values) {
            GetLogger().LogInformation("  [then] "+definition);
            assert(values);
        }

        public static void Then<T>(string definition,
            CheckActionWithSamples<T> assert, IEnumerable<T> values) {

            GetLogger().LogInformation("  [then] "+definition);
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
            try
            {
                assert();
                GetLogger().LogInformation("[.And] "+definition);
            } catch(Exception ex)
            {
                var lines = ex.Message.Split(Environment.NewLine.ToCharArray());
                var betterLines = lines.Skip(1).Select(line=>
                    "    " + line);
                var betterMessage = string.Join(Environment.NewLine,betterLines);
                GetLogger().LogError(
                    "[.And] "
                    +definition
                    + Environment.NewLine
                    + betterMessage);
            }

        }
    }
}