using System;
using System.Threading;
using System.Threading.Tasks;
using Iago.Runner.Setup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Iago.Runner
{
    public class AppHost : IHostedService
    {
        //private IFileSystemWatcher? watcher;
        private AppConfiguration appConfiguration;
        private ProjectWatcher _watcher;
        public AppHost(ISetupTheConfiguration setupTheConfiguration, IConfiguration config)
        {
            appConfiguration = SetupTheConfiguration
                .WithDefaults()
                .BuildConfiguration();
            Console.WriteLine("[working in] " + appConfiguration.WorkingDirectory);
            Console.WriteLine(JsonConvert.SerializeObject(config));
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
//            var watch = Task.Run(() =>
//            {
//                Console.WriteLine("working in  : " + Directory.GetCurrentDirectory());
////                var throttler = new MiniThrottler(20);
////                watcher = FileWatcherFactory.CreateWatcher(Directory.GetCurrentDirectory());
////                watcher.OnFileChange += (_, file) =>
////                {
////                    throttler.Throttle(file.GetHashCode().ToString(),file, (value) =>
////                    {
////                        Console.WriteLine($"Throttle - [{DateTime.Now}] {value} changed");
////                    });
////                    Console.WriteLine($"___ - [{DateTime.Now}] {file} changed");
////                };
//                
////                watcher.EnableRaisingEvents = true;
//                _watcher = new ProjectWatcher(appConfiguration);
//                Console.WriteLine("---- start listening for changes -----");
//            },cancellationToken);
            _watcher = new ProjectWatcher(appConfiguration);
            return Task.CompletedTask;
        }
 
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _watcher?.Dispose();
            return Task.CompletedTask;
        }
    }
}