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

    public class ConsoleHelper
    {
        public static Func<string> TokenizePattern {get;set;} =
            ()=>@"#(?<action>\w+)`(?<content>[^`]*)`";

        public static ConsoleStringToken[] Tokenize(string input, string pattern = null)
        {
            string currentPattern = pattern ?? TokenizePattern();
            if(!currentPattern.Contains("action")
                || !currentPattern.Contains("content"))
                {
                    throw new BadConsoleWriteTokenException(
                        "your pattern does not contain action, and content groups");
                }
            var rex = new Regex(currentPattern);
            Match match = rex.Match(input);
            var tokens = new List<ConsoleStringToken>();
            var bufferInput = input;
            var lastIndexEnd = 0;

            while(match.Success)
            {
                //match.GetType().GetProperties().ToList().ForEach(Console.WriteLine);
                if(match.Index != lastIndexEnd)
                {
                    tokens.Add(new ConsoleStringToken("normal",input.Substring(lastIndexEnd,(match.Index-lastIndexEnd))));
                }


                var actionName = match.Groups["action"].Value;
                var content = match.Groups["content"].Value;

                if(!string.IsNullOrEmpty(actionName))
                {
                    tokens.Add(new ConsoleStringToken(actionName,content));
                }
                lastIndexEnd = match.Index + match.Length;
                if(lastIndexEnd>input.Length) lastIndexEnd = input.Length;
                match = match.NextMatch();
            }
            return tokens.ToArray();
        }
    }

}
