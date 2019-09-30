using System;

namespace Iago.Runner.Setup
{
    public interface ISetupTheConfiguration
    {
        string CommandLineArguments { get; }
        SetupTheConfiguration DefineGetWorkingDirectory(Func<string> getWorkingDirectory);
        SetupTheConfiguration DefineIsWatching(Func<SetupTheConfiguration,bool> getIsWatching);
        AppConfiguration BuildConfiguration();
    }
}