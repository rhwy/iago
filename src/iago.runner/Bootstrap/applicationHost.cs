namespace Iago.Runner
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Reflection;
  using Iago.Abstractions;

  public delegate Type[] GetTypesWithTests(params string[] args);

  public class ApplicationHost
  {

    public HostedConfiguration Configuration {get;}
    private GetTypesWithTests getTypesWithTests;
    private readonly ILogger logger;

    public ApplicationHost(
      HostedConfiguration hostedConfiguration,
      ILogger logger = null)
    {
      Configuration = hostedConfiguration;
      this.logger = logger == null ? new SimpleConsoleLogger() : logger;
    }

    public void Run()
    {
      var hostedAssembly = Assembly.Load(Configuration.HostedApplicationName);
      logger.LogWarning(
        $"scanning assembly [{hostedAssembly.GetName().Name}]");

      var specTypes = new List<Type>();
      hostedAssembly.ExportedTypes
          .Where(type => type.Name.ToLower().EndsWith("specs"))
          .Where(type => type.IsClass)
          .ToList()
          .ForEach(type=> specTypes.Add(type));


        string specPlural = "Specification" +
            (specTypes.Count>1 ? "s" : "");
       logger.LogWarning($"found {specTypes.Count} {specPlural}");



        foreach(var spec in specTypes)
        {
          string name = spec.Name.Substring(0,spec.Name.Length-5);
          using(logger.BeginScope("running " + name + ""))
          {
            var instance = Activator.CreateInstance(spec);

            var specifyField = spec
                .GetFields(
                  BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(x=>x.FieldType == typeof(Specify));
            if(specifyField != null)
            {
              var specify = (specifyField.GetValue(instance) as Specify)?.Invoke();
              logger.LogInformation("=> " + specify);
              logger.LogInformation("");
            }
            var run = spec.GetMethod("Run");
            if(run != null)
            {
              try
              {
                run.Invoke(instance,null);
              } catch(Exception ex)
              {
                logger.LogError(ex.InnerException.Message);
              }

            }
          }
          logger.LogInformation("");
        }

    }
  }

}
