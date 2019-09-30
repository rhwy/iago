namespace Iago.Runner.Setup
{
    public class AppConfiguration
    {
        public string WorkingDirectory { get; }
        public bool IsWatching { get;}
        public bool PrintAllInfo { get;}

        public AppConfiguration(string workingDirectory, bool isWatching = false, bool printAllInfo = false)
        {
            WorkingDirectory = workingDirectory;
            IsWatching = isWatching;
            PrintAllInfo = printAllInfo;
        }
    }
}