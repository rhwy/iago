namespace Iago.WriterConsole
{
    using System;
    using static System.Console;
    
    public static class ConsoleHelper
    {
        private static void writeColor(string text, string color = "white")
        {
          ConsoleColor foregroundColor;
          if(Enum.TryParse<ConsoleColor>(color, true, out foregroundColor ))
          {
            Console.ForegroundColor = foregroundColor;
          }
          Write(text);
          Console.ResetColor();
        }
    }
}
