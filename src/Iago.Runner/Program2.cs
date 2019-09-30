using System;
using System.Threading.Tasks;
using Iago.Runner.Setup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Iago.Runner
{
    class Program2
    {
        static async Task Main(string[] args)
        {
            string _hostsettings = "hostsettings.json";
            string _prefix = "IAGO_";
            string _appsettings = "appsettings";
            
            var host = new HostBuilder()
                .ConfigureHostConfiguration(configHost =>
                {
                    configHost.SetBasePath(AppContext.BaseDirectory);
                    configHost.AddJsonFile(_hostsettings, optional: true);
                    configHost.AddEnvironmentVariables(prefix: _prefix);
                    configHost.AddCommandLine(args);
                })
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.SetBasePath(AppContext.BaseDirectory);
                    configApp.AddJsonFile(_appsettings + ".json", optional: true);
                    configApp.AddJsonFile(
                        $"{_appsettings}.{hostContext.HostingEnvironment.EnvironmentName}.json",
                        optional: true);
                    configApp.AddEnvironmentVariables(prefix: _prefix);
                    configApp.AddCommandLine(args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddLogging();
                    services.AddSingleton<ISetupTheConfiguration>(new SetupTheConfiguration(Environment.CommandLine));
                    //services.AddHostedService<AppHost>();
 
                })
                .ConfigureLogging((hostContext, configLogging) =>
                {
                    configLogging.AddConsole();
 
                })
                .UseConsoleLifetime()
                .Build();
            
            await host.RunAsync();
        }
    }
}