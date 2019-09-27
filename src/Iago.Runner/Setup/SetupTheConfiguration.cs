using System;

namespace Iago.Runner.Setup
{
    public class SetupTheConfiguration
    {
        private Func<string> _workingDirectory = null;
        public static SetupTheConfiguration New => new SetupTheConfiguration();

        public SetupTheConfiguration DefineGetWorkingDirectory(Func<string> getWorkingDirectory)
        {
            _workingDirectory = getWorkingDirectory;
            return this;
        }

        public AppConfiguration BuildConfiguration()
        {
            return new AppConfiguration(
                workingDirectory: _workingDirectory());
        }
    }

    public class AppConfiguration
    {
        public string WorkingDirectory { get; }

        public AppConfiguration(string workingDirectory)
        {
            WorkingDirectory = workingDirectory;
        }
    }
}