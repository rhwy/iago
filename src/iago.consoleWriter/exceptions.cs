namespace Iago.ConsoleWriter
{
    using System;

    public class BadConsoleWriteTokenException : Exception
    {
        public BadConsoleWriteTokenException(string message):base(message)
        {
        }
    }
}
