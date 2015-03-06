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

using static System.Console;


namespace Iago.Runner {

  public enum Option { None,Some}

  public class Program {
    IApplicationEnvironment environment;
    IServiceProvider serviceProvider;
    Configuration configuration;
    ApplicationHost app;
    ILogger logger =  new SimpleConsoleLogger();

    public void Main(params string[] args)
    {

      WriteLine("== --------------------------------------------------");
      Write("==     ");
      writeColor("IAGO - K Spec Runner ","magenta");
      writeColor(Environment.NewLine,"gray");
      WriteLine($"== -----------------------------v0.1.0-beta4-3------");
      WriteLine("");
      app.Run();
    }


    public Program(
      IAssemblyLoaderContainer container,
      IApplicationEnvironment appEnv,
      IServiceProvider services)
    {
      Iago.Specs.SetLogger(()=> logger);
      app = new ApplicationHost(
        setupHostedConfiguration(appEnv),
        logger
      );
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

    private static HostedConfiguration setupHostedConfiguration(IApplicationEnvironment appEnv)
    {
      return new HostedConfiguration(
        path : appEnv.ApplicationBasePath,
        name : appEnv.ApplicationName,
        config : appEnv.Configuration,
        version : appEnv.Version
      );
    }

  }


}
