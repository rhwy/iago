namespace Iago.Runner
{
  public struct HostedConfiguration
  {
      public string ExecutionPath {get;}
      public string HostedApplicationName {get;}
      public string ExecutionConfiguration {get;}
      public string HostedApplicationVersion {get;}
      public string AppVersion {get;}

      public HostedConfiguration(
        string path = "",
        string name = "",
        string config = "",
        string hostVersion = "",
        string appVersion = ""
      ) {
        ExecutionPath = path;
        HostedApplicationName = name;
        ExecutionConfiguration = config;
        HostedApplicationVersion = hostVersion;
        AppVersion = appVersion;
      }
  }
}
