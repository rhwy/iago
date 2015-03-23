using System;
using System.IO;
using Microsoft.Framework.Runtime;
//using Microsoft.Framework.Runtime.Infrastructure;
using Microsoft.Framework.DependencyInjection;
//using Microsoft.Framework.DependencyInjection.Fallback;
//using Microsoft.Framework.DependencyInjection.ServiceLookup;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.ConfigurationModel.Json;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Console;
using ILogger = Microsoft.Framework.Logging.ILogger;
using System.Linq;
using System.Collections.Generic;
using static System.Console;


namespace Iago.Runner {

  public enum Option { None,Some}

  public class Program {
    IApplicationEnvironment environment;
    HostedConfiguration hostConfig;
    Configuration configuration;
    ApplicationHost app;
    ILogger logger =  new SimpleConsoleLogger();

    public void Main(params string[] args)
    {

      WriteLine("== --------------------------------------------------");
      Write("==     ");
      writeColor("IAGO - K Spec Runner ","magenta");
      writeColor(Environment.NewLine,"gray");
      WriteLine($"== -----------------------------{hostConfig.AppVersion}------");
      WriteLine("");

      try
      {
          app.Run();
      }
      catch(Exception cex)
      {
        if (cex.GetType().Name == "RoslynCompilationException")
            writeColor("[FAIL] compilation exception, please fix to run tests"+Environment.NewLine,"red");
        else
            writeColor($"[FAIL] {cex.GetType().Name}, please fix before running tests"+Environment.NewLine,"red");

        writeColor("[FAIL] " + cex.Message + Environment.NewLine,"red");
      }
    }


    public Program(IApplicationEnvironment appEnv)
    {
      Iago.Specs.SetLogger(()=> logger);

      environment = appEnv;
      var asm = System.Reflection.Assembly.GetExecutingAssembly();
      var appVersion = asm.GetName().Version.ToString();

      hostConfig = setupHostedConfiguration(appEnv,appVersion);
      app = new ApplicationHost(hostConfig,logger);

      //var values = Enum.GetValues(typeof(LogLevel));
      //foreach(var val in values ) Console.WriteLine(val);
    }

    private static void writeColor(string text, string color = "white")
    {
      ConsoleColor foregroundColor;
      if(Enum.TryParse<ConsoleColor>(color, true, out foregroundColor ))
      {
        Console.ForegroundColor = foregroundColor;
      }
      Write(text);
      Console.ResetColor();
    }

    private static HostedConfiguration setupHostedConfiguration(
        IApplicationEnvironment appEnv, string appVersion)
    {
        var asm = System.Reflection.Assembly.GetExecutingAssembly();
        WriteLine(asm.GetName().Version);

        return new HostedConfiguration(
            path : appEnv.ApplicationBasePath,
            name : appEnv.ApplicationName,
            config : appEnv.Configuration,
            hostVersion : appEnv.Version,
            appVersion : appVersion
        );
    }

  }


}
