namespace Watchbird
{
	using System;
	using static System.Console;
	
	using Microsoft.Framework.Configuration;
	using Microsoft.Framework.Runtime;
	using Utilities;
	
	using FSWatcher;
	using System.Diagnostics;
	
	
	public class BirdEye
	{
		
	}
	public class Program
	{
		IApplicationEnvironment environment;
		Action<string> log = (m) => {
			var currentColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Write($"[{ApplicationTime.GetStamp()}]"); 
			Console.ForegroundColor = currentColor;
			WriteLine($" {m}");
		};
		
		public Program(IApplicationEnvironment appEnv)
	    {
	 
	      environment = appEnv;
		  ApplicationTime.Start();
	      var asm = System.Reflection.Assembly.GetExecutingAssembly();
	      var appVersion = asm.GetName().Version.ToString(); 
	      
		  WriteLine($"=========== DNX Watcher, v{appVersion} ==========");
		  
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
					 log($"change -> {fsElement}");
					 var started = ApplicationTime.Now;
					 
					 if(!string.IsNullOrEmpty(dnx))
					 {
						 log($"ready to {dnx}");
						 
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
					             log($"Done in {stoped}ms");
				            }
						 }
					
					 } else {
						 var cmd = configuration.Get("cmd:file");
						 var cmdArgs = configuration.Get("cmd:args");
						 if(!string.IsNullOrEmpty(cmd))
						 {
							 log($"starting command [{cmd}]");
							 
							 var info = new ProcessStartInfo(
								 filename:cmd,
								 arguments:cmdArgs
							 );
							 lock (Console.Out)
							 {
								 using(Process process = Process.Start(info))
								 {
									 process.EnableRaisingEvents = true;
									 process.WaitForExit();
					            	 var stoped = (ApplicationTime.Now - started).TotalMilliseconds;
						             log($"Done in {stoped}ms");
					            }
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
			onChange("Start");
		    
			Func<ConsoleKeyInfo,bool> checkKey = 
			(key)=>{
				return key.Key != ConsoleKey.Escape;	
			};
			
            ConsoleKeyInfo cki;
		    Console.TreatControlCAsInput = true;
			WriteLine("Press [ESQ] to stop watching");
		    do {
		         cki = Console.ReadKey(false);
		   } while (checkKey(cki));
	  
	  
			watcher.StopWatching();
		}
	}
}