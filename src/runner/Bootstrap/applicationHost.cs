namespace Iago.Runner
{
  public class ApplicationHost
  {
    public HostedConfiguration Configuration {get;}

    public ApplicationHost(HostedConfiguration hostedConfiguration)
    {
      Configuration = hostedConfiguration;
    }

    public void Run()
    {
      var domainAssemblies = AppDomain.CurrentDomain.GetAssemblies();
    }
  }

}
