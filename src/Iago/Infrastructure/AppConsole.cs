using System;
using System.IO;
using System.Text;

namespace Iago.Common
{
    public enum AppConsoleWriteMode
    {
        WriteConsoleAndLog,
        WriteLog
    }
    
    public class AppConsole
    {
        private readonly StringBuilder log;
        private readonly StringWriter sw;

        public AppConsole()
        {
            log = new StringBuilder();
            sw = new StringWriter(log);
        }

        private AppConsoleWriteMode Mode {get;set;}
          = AppConsoleWriteMode.WriteConsoleAndLog;

        private string Log => log.ToString();

        public string Write(object message)
        {
            var currentOut = Console.Out;
            Console.SetOut(sw);
            Console.Write(message);
            Console.SetOut(currentOut);
            if(Mode == AppConsoleWriteMode.WriteConsoleAndLog)
              Console.Write(message);

            return message?.ToString();
        }
        public string WriteLine(object message)
        {
            var currentOut = Console.Out;
            Console.SetOut(sw);
            Console.WriteLine(message);
            Console.SetOut(currentOut);
            if(Mode == AppConsoleWriteMode.WriteConsoleAndLog)
              Console.WriteLine(message);

            return message?.ToString();
        }

        public string Save(Action<string> saveTo)
        {
            saveTo?.Invoke(Log);
            return Log;
        }
    }
}
