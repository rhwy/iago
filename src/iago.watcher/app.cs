namespace Iago.FolderMonitor
{
	using System;
	using static System.Console;
	
	using Microsoft.Framework.Configuration;
	using Microsoft.Framework.Runtime;
	using Utilities;
	
	using FSWatcher;
	using System.Diagnostics;
	
	
	public class Program
	{
		IApplicationEnvironment environment;
		static DateTime start = ApplicationTime.Now;
		static Func<string> stamp = () => new DateTime((ApplicationTime.Now-start).Ticks).ToString("ss:ffffff");
		
		Action<string> log = (m) => {
			WriteLine($"[{stamp()}] {m}"); 
		};
		
		public Program(IApplicationEnvironment appEnv)
	    {
	 
	      environment = appEnv;
		  
	      var asm = System.Reflection.Assembly.GetExecutingAssembly();
	      var appVersion = asm.GetName().Version.ToString(); 
	      
		  WriteLine($"==== DNX Watcher, v{appVersion} ===");
		  
		  log($"watch folder : {appEnv.ApplicationBasePath}");
		  log("started watch");
	    }
		
		
		public void Main(string[] args)
		{
			var configurationBuilder = new ConfigurationBuilder(
					environment.ApplicationBasePath)
          		.AddJsonFile("watcher.json",true)
				.AddCommandLine(args)
				;
				
        	IConfiguration configuration = configurationBuilder.Build();
         	 
			 Action<string> onChange = 
			 	(fsElement) => {
					 var dnx = configuration.Get("dnx");
					 var started = ApplicationTime.Now;
					 
					 if(!string.IsNullOrEmpty(dnx))
					 {
						 WriteLine($"ready to {dnx}");
						 
						 var info = new ProcessStartInfo(
							 filename:"dnx",
							 arguments:environment.ApplicationBasePath + " " +dnx
						 );
						 lock (Console.Out)
						 {
							 using(Process process = Process.Start(info))
							 {
								 process.EnableRaisingEvents = true;
								 process.WaitForExit();
				            	 var stoped = (ApplicationTime.Now - started).TotalMilliseconds;
					             Console.WriteLine($"Done in {stoped}ms");
				            }
						 }
					 }
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
    
			int pollFreq;
			if(int.TryParse(configuration.Get("pollFrequency"), out pollFreq))
			{
				watcher.Settings.SetPollFrequencyTo(pollFreq); 
			}
			
			watcher.Watch();
			onChange("start");
		    
			Func<ConsoleKeyInfo,bool> checkKey = 
			(key)=>{
				return key.Key != ConsoleKey.Escape;	
			};
			
            ConsoleKeyInfo cki;
		    Console.TreatControlCAsInput = true;
		
		    do {
		         cki = Console.ReadKey(false);
		   } while (checkKey(cki));
	  
	  
			watcher.StopWatching();
		}
	}
}