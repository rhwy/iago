namespace Iago.ConsoleWriter
{
    using System.Text.RegularExpressions;
    using System.Collections.Generic;
    using System;

    public class BadConsoleWriteTokenException : Exception
    {
        public BadConsoleWriteTokenException(string message):base(message)
        {
        }
    }
}
