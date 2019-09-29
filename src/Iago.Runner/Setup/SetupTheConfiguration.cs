using System;
using System.IO;

namespace Iago.Runner.Setup
{
    public class SetupTheConfiguration
    {
        private Func<string> _workingDirectory = null;
        private Func<SetupTheConfiguration, bool> _isWatching = 
            (s) => s.CommandLineArguments?.ToLower().Contains("watch") ?? false;
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
                isWatching: _isWatching?.Invoke(this) ?? false);
        }

        public static SetupTheConfiguration WithDefaults(string args = null)
        {
            return new SetupTheConfiguration(args ?? string.Empty)
                .DefineGetWorkingDirectory(Directory.GetCurrentDirectory);
        }
    }

    public class AppConfiguration
    {
        public string WorkingDirectory { get; }
        public bool IsWatching { get;}

        public AppConfiguration(string workingDirectory, bool isWatching = false)
        {
            WorkingDirectory = workingDirectory;
            IsWatching = isWatching;
        }
    }
}