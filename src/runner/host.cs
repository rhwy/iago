
namespace Iago.Runner {
  using System;
  using System.IO;
  using Microsoft.Framework.Runtime;
  using Microsoft.Framework.Runtime.Infrastructure;
  using Microsoft.Framework.DependencyInjection;
  using Microsoft.Framework.DependencyInjection.Fallback;
  using Microsoft.Framework.DependencyInjection.ServiceLookup;
  using Microsoft.Framework.ConfigurationModel;
  using Microsoft.Framework.ConfigurationModel.Json;
  using Microsoft.AspNet.Hosting;
  using Microsoft.AspNet.Builder;

  using System.Linq;

  using static System.Console;


  public class Program {
    IApplicationEnvironment environment;
    IServiceProvider serviceProvider;
    Configuration configuration;
    ApplicationHost app;

    public void Main(params string[] args)
    {

      WriteLine($"---------------{app.Configuration.HostedApplicationName}--------------");

    }


    public Program(
      IAssemblyLoaderContainer container,
      IApplicationEnvironment appEnv,
      IServiceProvider services)
    {

      app = new ApplicationHost(
              new HostedConfiguration(
                path : AppDomain.CurrentDomain.BaseDirectory,
                name : "Iago Runner"
              ));
    }


/* removed until IApplicationEnvironment will be injected correctly
    private static HostedConfiguration setupHostedConfiguration(IApplicationEnvironment appEnv)
    {
      return new HostedConfiguration(
        path : appEnv.ApplicationBasePath,
        name : appEnv.ApplicationName,
        config : appEnv.Configuration,
        version : appEnv.Version
      );
    }
    */    
  }


}
