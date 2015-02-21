namespace Iago.Runner
{
  public class ApplicationHost
  {
    public HostedConfiguration Configuration {get;}

    public ApplicationHost(HostedConfiguration hostedConfiguration)
    {
      Configuration = hostedConfiguration;
    }
  }

}
