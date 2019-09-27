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
        }
        
    }
}