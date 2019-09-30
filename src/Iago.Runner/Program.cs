using System;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using Enlil;
using Iago.Language;
using Iago.Runner.Setup;
using Nabu;
using Nabu.TextStyling;
using static Enlil.AssemblyHelper;

namespace Iago.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var info = ConsoleStyles.Build("#blue", new ColorPaletteMonokai());
            
            var asm = System.Reflection.Assembly.GetExecutingAssembly();
            var appVersion = asm.GetName().Version.ToString();
            Console.WriteLine(info(IagoHeader.GetHeader(appVersion,Console.WindowWidth)));
            
            var appconfig = SetupTheConfiguration
                    .WithDefaults(Environment.CommandLine)
                .BuildConfiguration();
            
            var runner = new Runner(appconfig);
            runner.Run();

            Console.WriteLine(info("=".Replicate(Console.WindowWidth)));
            Console.WriteLine(info(">>> done <<<".Center(Console.WindowWidth)));
            Console.WriteLine(info("=".Replicate(Console.WindowWidth)));
            
        }
    }


    public class Runner
    {
        private readonly AppConfiguration _configuration;

        public Runner(AppConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Run()
        {
            var projectHelper = new ProjectHelper(_configuration.WorkingDirectory);
            var buildContext = projectHelper.BuildProjectAssembly();
            var asm = buildContext.ResultingAssembly;
            var specs = asm.GetExportedTypes().Where(t => t.Name.ToLower().EndsWith("specs"));
            if (specs.Any())
            {
                Console.WriteLine("found specs : ");
                foreach (var spec in specs)
                {
                    
                    var fields = spec.GetFields(BindingFlags.NonPublic);
                    var that = spec
                        .GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
                        .FirstOrDefault(t => t.FieldType == typeof(Specify));
                    var run = spec
                        .GetMethod("Run");
                    
                    if (that != default && run != null)
                    {
                        var instance = Activator.CreateInstance(spec);
                        Console.WriteLine($"[{spec.Name}]");
                        var thatValue = ((Specify)that.GetValue(instance))();
                        Console.WriteLine($"    {thatValue}");

                        try
                        {
                            run.Invoke(instance, null);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            
                        }
                        
                    }
                }
            }
            
        }
    }
}
