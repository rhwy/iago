namespace Iago.Runner {
  using System;
  using System.IO;
  using Microsoft.Dnx.Runtime;
  using Microsoft.Framework.Configuration;
  using Iago.Abstractions;
  public enum Option { None,Some}

  public class Program {
    IApplicationEnvironment environment;
    HostedConfiguration hostConfig;
    IConfiguration configuration;
    ApplicationHost app;

    AppConsole appConsole = new AppConsole();

    ILogger logger =  new SimpleConsoleLogger();


    public void Main(params string[] args)
    {
        var configurationBuilder = new ConfigurationBuilder()
          .SetBasePath(environment.ApplicationBasePath)
          .AddCommandLine(args);
          
        configuration = configurationBuilder.Build();
        
      var version = $"v{hostConfig.AppVersion}";

      writeColor(IagoHeader.GetHeader(version),"cyan");
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

      var save = configuration["out:file"];
      if(!string.IsNullOrEmpty(save))
      {
          if(!save.StartsWith("/"))
          {
              save = Path.Combine(environment.ApplicationBasePath,save);
          }
          appConsole.Save((log)=> File.WriteAllText(save,log));
      }
    }


    public Program(IApplicationEnvironment appEnv)
    {
 
      Iago.Specs.SetLogger(()=> logger);
      
      /*
      IApplicationEnvironment properties:
        ApplicationName
        ApplicationBasePath
        ApplicationVersion
        Configuration
        RuntimeFramework
      */
      
      environment = appEnv;
      var asm = System.Reflection.Assembly.GetExecutingAssembly();
      var appVersion = asm.GetName().Version.ToString();
      
      hostConfig = setupHostedConfiguration(appEnv,appVersion);
      app = new ApplicationHost(hostConfig,logger);
      
    }

    private void writeColor(string text, string color = "white")
    {
      ConsoleColor foregroundColor;
      if(Enum.TryParse<ConsoleColor>(color, true, out foregroundColor ))
      {
        Console.ForegroundColor = foregroundColor;
      }
      appConsole.Write(text);
      Console.ResetColor();
    }

    private static HostedConfiguration setupHostedConfiguration(
        IApplicationEnvironment appEnv, string appVersion)
    {
      HostedConfiguration config;
       try
       {
         config = new HostedConfiguration(
            path : appEnv.ApplicationBasePath,
            name : appEnv.ApplicationName,
            config : appEnv.Configuration,
            hostVersion : appEnv.ApplicationVersion,
            appVersion : appVersion
        );
       }
       catch (System.Exception e) 
       {
         config = new HostedConfiguration();
         Console.WriteLine(e.Message);
         Console.WriteLine(appEnv);
       }
       return config;
        
    }

  }


}
