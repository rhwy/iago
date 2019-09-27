using Iago.Language;
using NFluent;

namespace Iago.Tests
{
    using Xunit;
    using static Iago.Language.Specs;

    public partial class GivenLanguageKeywords
    {
        public class WhenUsing_DescribeMethod
        {
            private TestLogger testLogger;

            public WhenUsing_DescribeMethod()
            {
                this.testLogger = new TestLogger();
                Specs.SetLogger(()=>testLogger);
            }
            
            [Fact]
            public void it_should_execute_action_on_It_keyword()
            {
                var counter = 0;
                It(" should set counter", () =>
                {
                    counter = 42;
                });
                Check.That(counter).IsEqualTo(42);
                Check.That(testLogger.LogLines.Pop())
                    .IsEqualTo("[Information]  [describe]  should set counter");
            }
            
            [Fact]
            public void it_should_execute_action_on_Describe_keyword()
            {
                var counter = 0;
                Describe(" should set counter", () =>
                {
                    counter = 42;
                });
                Check.That(counter).IsEqualTo(42);
                Check.That(testLogger.LogLines.Pop())
                    .IsEqualTo("[Information]  [it]  should set counter");
            }
        }
        
    }
}
