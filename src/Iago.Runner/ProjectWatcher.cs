using System;
using System.IO;
using System.Linq;
using Iago.Runner.Runtime;
using Iago.Runner.Setup;
using Microsoft.DotNet.Watcher.Internal;

namespace Iago.Runner
{
    public class ProjectWatcher : IDisposable
    {
        private IFileSystemWatcher? _watcher;
        private AppConfiguration _configuration;
        public ProjectWatcher(AppConfiguration configuration)
        {
            _configuration = configuration;
            var throttler = new MiniThrottler(200);
            _watcher = FileWatcherFactory.CreateWatcher(_configuration.WorkingDirectory);
            var runner = new Runner(configuration);
            var ignoreFiles = new[] {"obj/", "bin/","___jb_tmp___","___jb_old___"}
                .Select(i => Path.Combine(_configuration.WorkingDirectory, i));
            bool ongoingChange = false;
            _watcher.OnFileChange += (_, file) =>
            {
                if (ignoreFiles.Any(x => file.Contains(x))) return;
                
//                throttler.Throttle(_configuration.WorkingDirectory.GetHashCode().ToString(),
//                    file, 
//                    (value) =>
//                {
//                    Console.WriteLine($"detected change - [{DateTime.Now}] {value}");
//                    runner.Run();
//                });

                if (ongoingChange) return;
                ongoingChange = true;
                runner.Run();
                ongoingChange = false;
            };
            _watcher.EnableRaisingEvents = _configuration.IsWatching;
            runner.Run();
        }

        public void Dispose()
        {
            _watcher?.Dispose();
        }
    }
}