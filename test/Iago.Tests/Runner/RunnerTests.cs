using System;
using System.IO;
using Iago.Runner.Setup;
using NFluent;
using Xunit;

namespace Iago.Tests.Runner
{
    public partial class RunnerTests
    {
        public class GivenSetupTheConfiguration
        {
            [Fact]
            public void setup_provides_a_setter_for_current_dir()
            {
                AppConfiguration appConfiguration = SetupTheConfiguration
                    .New
                    .DefineGetWorkingDirectory(() => "/path/test")
                    .BuildConfiguration();

                Check.That(appConfiguration.WorkingDirectory).IsEqualTo("/path/test");
            }
            
            [Fact]
            public void setup_provides_defaults_helpers()
            {
                AppConfiguration appConfiguration = SetupTheConfiguration
                    .WithDefaults()
                    .BuildConfiguration();

                Check.That(appConfiguration.WorkingDirectory).IsEqualTo(Directory.GetCurrentDirectory());
            }
            
            [Fact]
            public void setup_provides_info_about_watch()
            {
                var commandLine = "dotnet iago watch run";
                AppConfiguration appConfiguration = SetupTheConfiguration
                    .WithDefaults(commandLine)
                    .BuildConfiguration();

                Check.That(appConfiguration.IsWatching).IsTrue();
            }
        }
        
    }
}