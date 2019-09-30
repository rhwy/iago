using System;
using System.IO;

namespace Iago.Runner.Setup
{
    public class SetupTheConfiguration : ISetupTheConfiguration
    {
        private Func<string> _workingDirectory = Directory.GetCurrentDirectory;
        private Func<SetupTheConfiguration, bool> _isWatching = 
            (s) => s.CommandLineArguments?.ToLower().Contains("watch") ?? false;

        private Func<SetupTheConfiguration, bool> _isVerbose =
            (s) => s.CommandLineArguments?.ToLower().Contains("verbose") ?? false;
        public string CommandLineArguments { get; }
        public static SetupTheConfiguration New => new SetupTheConfiguration();

        private SetupTheConfiguration()
        {
            CommandLineArguments = string.Empty;
        }

        public SetupTheConfiguration(string args)
        {
            CommandLineArguments = args;
        }
        
        public SetupTheConfiguration DefineGetWorkingDirectory(Func<string> getWorkingDirectory)
        {
            _workingDirectory = getWorkingDirectory;
            return this;
        }
        public SetupTheConfiguration DefineIsWatching(Func<SetupTheConfiguration,bool> getIsWatching)
        {
            _isWatching = getIsWatching;
            return this;
        }

        public AppConfiguration BuildConfiguration()
        {
            return new AppConfiguration(
                workingDirectory: _workingDirectory?.Invoke(),
                isWatching: _isWatching?.Invoke(this) ?? false,
                printAllInfo:_isVerbose?.Invoke(this) ?? false);
        }

        public static SetupTheConfiguration WithDefaults(string args = null)
        {
            return new SetupTheConfiguration(args ?? string.Empty);
        }
    }
}