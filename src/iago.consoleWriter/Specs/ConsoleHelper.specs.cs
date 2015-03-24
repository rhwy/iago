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
                  Sample(input:input,output:expected.ToEachLineString());

                  var tokens = ConsoleHelper.Tokenize(input);

                  Check.That(tokens.Properties("Action"))
                    .ContainsExactly(expected.Select(x=>x.Action));

                  Check.That(tokens.Properties("State"))
                    .ContainsExactly(expected.Select(x=>x.State));

              });
          });

      }
    }
}
