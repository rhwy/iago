namespace Iago.FolderMonitor
{
	using System;
	using static System.Console;
	
	using Microsoft.Framework.Configuration;
	using Microsoft.Framework.Runtime;
	using Utilities;
	
	using FSWatcher;
	
	
	
	public class Program
	{
		IApplicationEnvironment environment;
		
		public Program(IApplicationEnvironment appEnv)
	    {
	 
	      environment = appEnv;
		  var start = ApplicationTime.Now;
	      var asm = System.Reflection.Assembly.GetExecutingAssembly();
	      var appVersion = asm.GetName().Version.ToString(); 
	      WriteLine($"==== DNX Watcher, v{appVersion} ===");
		  Func<string> stamp = () => new DateTime((ApplicationTime.Now-start).Ticks).ToString("ss:ffffff");
		  WriteLine($"[{stamp()}] watch folder : {appEnv.ApplicationBasePath}");
		  WriteLine($"[{stamp()}] started watch"); 
	    }
		public void Main(string[] args)
		{
			var configurationBuilder = new ConfigurationBuilder(
					environment.ApplicationBasePath)
          		.AddJsonFile("watcher.json")
				.AddCommandLine(args)
				;
				
        	IConfiguration configuration = configurationBuilder.Build();
         	
			 Action<string> onChange = 
			 	(fsElement) => {
					 WriteLine(fsElement);
				 };
			var watcher = 
				new Watcher(
					dir: Environment.CurrentDirectory,
					directoryCreated : onChange,
					directoryDeleted:onChange,
					fileCreated:onChange,
					fileChanged:onChange,
					fileDeleted:onChange);
					
			watcher.ErrorNotifier((path, ex) => { System.Console.WriteLine("{0}\n{1}", path, ex); });
    
			WriteLine("key:"+configuration.Get("pollFrequency"));
			int pollFreq;
			if(int.TryParse(configuration.Get("pollFrequency"), out pollFreq))
			{
				WriteLine("freq:{0}",pollFreq);
				watcher.Settings.SetPollFrequencyTo(pollFreq); 
				WriteLine("freq:{0}",watcher.Settings.PollFrequency);
			}
			
			watcher.Watch();
			var command = System.Console.ReadLine();
            
			watcher.StopWatching();
		}
	}
}