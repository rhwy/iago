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
            
            [Theory]
            [InlineData("dotnet iago", false)]
            [InlineData("dotnet iago watch", true)]
            public void setup_provides_info_about_watch(string args, bool expected)
            {
                AppConfiguration appConfiguration = SetupTheConfiguration
                    .WithDefaults(args)
                    .BuildConfiguration();

                Check.That(appConfiguration.IsWatching).IsEqualTo(expected);
            }
            
            [Theory]
            [InlineData("dotnet iago", false)]
            [InlineData("dotnet iago watch verbose", true)]
            public void setup_provides_verbose_option_to_print_all(string args, bool expected)
            {
                AppConfiguration appConfiguration = SetupTheConfiguration
                    .WithDefaults(args)
                    .BuildConfiguration();

                Check.That(appConfiguration.PrintAllInfo).IsEqualTo(expected);
            }
        }
        
    }
}