namespace Iago
{
    using System;
    using System.IO;
    using System.Text;
    
    public enum AppConsoleWriteMode
    {
        WriteConsoleAndLog,
        WriteLog
    }
    public class AppConsole
    {
        StringBuilder log;
        StringWriter tw;

        public AppConsole()
        {
            log = new StringBuilder();
            tw = new StringWriter(log);
        }

        public AppConsoleWriteMode Mode {get;set;}
          = AppConsoleWriteMode.WriteConsoleAndLog;

        public string Log { get { return log.ToString();}}

        public string Write(string message)
        {
            var currentOut = Console.Out;
            Console.SetOut(tw);
            Console.Write(message);
            Console.SetOut(currentOut);
            if(Mode == AppConsoleWriteMode.WriteConsoleAndLog)
              Console.Write(message);

            return message;
        }
        public string WriteLine(string message)
        {
            var currentOut = Console.Out;
            Console.SetOut(tw);
            Console.WriteLine(message);
            Console.SetOut(currentOut);
            if(Mode == AppConsoleWriteMode.WriteConsoleAndLog)
              Console.WriteLine(message);

            return message;
        }

        public string Save(Action<string> saveTo)
        {
            if(saveTo != null)
              saveTo(Log);
            return Log;
        }
    }
}
