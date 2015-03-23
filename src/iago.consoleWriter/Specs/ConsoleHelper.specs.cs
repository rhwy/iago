namespace Iago.ConsoleWriter.Specs
{
    using Iago;
    using static Iago.Specs;
    using NFluent;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System;
    using System.Linq;

    public class ConsoleHelperSpecs
    {
      Specify that = () =>
          "It provides high order functions for helping console write";

      public void Run()
      {
          Describe("tokenizeString", ()=> {

              It("extracts write tokens from string", ()=>{

                  string input = "Message : #green`[info]` say #yellow`hello` again";
                  var expected = new []{
                    new ConsoleStringToken("normal","Message : "),
                    new ConsoleStringToken("green","[info]"),
                    new ConsoleStringToken("normal"," say "),
                    new ConsoleStringToken("yellow","hello")
                  };

                  var tokens = ConsoleHelper.Tokenize(input);
                  Check.That(tokens.Properties("Action"))
                    .ContainsExactly(expected.Select(x=>x.Action));
                  Check.That(tokens.Properties("State"))
                      .ContainsExactly(expected.Select(x=>x.State));
              });
          });

      }
    }

    public class ConsoleHelper
    {
        public static ConsoleStringToken[] Tokenize(string input)
        {
            string pattern = @"#(?<action>\w+)`(?<content>[^`]*)`";
            var rex = new Regex(pattern);
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

    public class ConsoleStringToken
    {
        public string Action {get;}
        public string State {get;}
        public ConsoleStringToken(string action, string state)
        {
            Action = action;
            State = state;
        }
    }
}
