namespace Iago.Runner
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Reflection;
  using static System.Console;

  public class ApplicationHost
  {
    public HostedConfiguration Configuration {get;}

    public ApplicationHost(HostedConfiguration hostedConfiguration)
    {
      Configuration = hostedConfiguration;
    }

    public void Run()
    {
      //var domainAssemblies = AppDomain.CurrentDomain.GetAssemblies();
      var domainAssemblies = new []{ Assembly.Load(Configuration.HostedApplicationName)};
      var specTypes = new List<Type>();
      WriteLine("scanning ...");
      foreach(var asm in domainAssemblies)
      {
        WriteLine("--> " + asm.FullName);
        asm.ExportedTypes
          .Where(type => type.Name.ToLower().EndsWith("specs"))
          .ToList()
          .ForEach(type=> specTypes.Add(type));
      }

      WriteLine("specs found:");
      foreach(var spec in specTypes)
      {
        WriteLine("\t - " + spec.Name);
      }
    }
  }

}
