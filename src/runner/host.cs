using System;
using System.IO;
using Microsoft.Framework.Runtime;
//using Microsoft.Framework.Runtime.Infrastructure;
using Microsoft.Framework.DependencyInjection;
//using Microsoft.Framework.DependencyInjection.Fallback;
//using Microsoft.Framework.DependencyInjection.ServiceLookup;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.ConfigurationModel.Json;
//using Microsoft.AspNet.Hosting;
//using Microsoft.AspNet.Builder;

using System.Linq;

using static System.Console;


namespace Iago.Runner {


  public class Program {
    IApplicationEnvironment environment;
    IServiceProvider serviceProvider;
    Configuration configuration;
    ApplicationHost app;

    public void Main(params string[] args)
    {

      WriteLine("== --------------------------------------------------");
      Write("==     ");
      writeColor("IAGO - K Spec Runner ","magenta");
      writeColor(Environment.NewLine,"gray");
      WriteLine($"== -----------------------------v0.1.0-beta4-3------");

      writeColor("[info] ","green");
      WriteLine($"Testing [{app.Configuration.HostedApplicationName}]");
      app.Run();
      /*var c = new Configuration().AddJsonFile("config.json");
      WriteLine($"name = {c.Get("name")}");*/
    }


    public Program(
      IAssemblyLoaderContainer container,
      IApplicationEnvironment appEnv,
      IServiceProvider services)
    {
      //WriteLine($"app name : {appEnv.ApplicationBasePath}");

      app = new ApplicationHost(
        setupHostedConfiguration(appEnv)
      );
    }

    private static void writeColor(string text, string color = "white")
    {
      var current = Console.ForegroundColor;
      ConsoleColor foregroundColor = current;
      if(Enum.TryParse<ConsoleColor>(color, true, out foregroundColor ))
      {
        Console.ForegroundColor = foregroundColor;
      }
      Write(text);
      Console.ForegroundColor = current;
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
