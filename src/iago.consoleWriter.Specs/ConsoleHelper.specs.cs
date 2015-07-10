namespace Iago.ConsoleWriter.Specs
{
    using Iago;
    using static Iago.Specs;
    using NFluent;
    using System.Linq;
    using System;

    public class ConsoleTokensSpecs
    {
        Specify that = () =>
          "it defines a serie of qualified commands";


    }


    

    public class OutputWriteSpecs
    {
        Specify that = ()=>
            "it defines a chainable command for writing things to outputs";

        public void Run()
        {
            Describe("default usage",()=>{

                It("builds a source writer from Factory and passes it to then",()=>{
                    //string input = "Message : #green`[info]` say #yellow`hello` again";
                    string input = "123456789";
                    var result = OutputWrite<string>
                        .Source(input)
                        .Then(f=>f.Count());
    
                    Check.That(result).IsEqualTo(9);
                });
                

            });
        }
    }

    public class ConsoleHelperSpecs
    {
      Specify that = () =>
          "It provides high order functions for simplify console writing";

      public void Run()
      {
          Describe("function TokenizeString", ()=> {

              It("extracts write tokens from string", ()=>{

                  string input = "Message : #green`[info]` say #yellow`hello` again";
                  var expected = new []{
                    new ConsoleStringToken("normal","Message : "),
                    new ConsoleStringToken("green","[info]"),
                    new ConsoleStringToken("normal"," say "),
                    new ConsoleStringToken("yellow","hello")
                  };

                  var tokens = ConsoleHelper.Tokenize(input);
                  Sample(input:input,output:tokens.ToEachLineString());

                  Check.That(tokens.Extracting("Action"))
                    .ContainsExactly(expected.Select(x=>x.Action));

                  Check.That(tokens.Extracting("State"))
                    .ContainsExactly(expected.Select(x=>x.State));

              });

              It("can uses optionnal pattern to find tokens", () => {
                 string input = "say §green<hello>";
                 string pattern = @"§(?<action>\w+)<(?<content>[^>]*)>";
                 var expected = new []{
                   new ConsoleStringToken("normal","say "),
                   new ConsoleStringToken("green","hello")
                 };

                 var tokens = ConsoleHelper.Tokenize(input,pattern);
                 Sample(
                    context:pattern,
                    input:input,
                    output:tokens.ToEachLineString());

                 Check.That(tokens.Extracting("Action"))
                   .ContainsExactly(expected.Select(x=>x.Action));

                 Check.That(tokens.Extracting("State"))
                   .ContainsExactly(expected.Select(x=>x.State));
              });

              It("must ensure optional pattern contains 'action','content' capture groups",()=>{
                  Check.ThatCode(()=>{
                      var tokens = ConsoleHelper.Tokenize("any input","bad pattern without groups");
                  }).Throws<BadConsoleWriteTokenException>();
              });

              It("uses external [TokenizePattern] value as default", ()=>{
                  ConsoleHelper.TokenizePattern = () => @"§(?<action>\w+)<(?<content>[^>]*)>";

                  string input = "hello §red<world>";
                  var expected = new []{
                    new ConsoleStringToken("normal","hello "),
                    new ConsoleStringToken("red","world")
                  };

                  var tokens = ConsoleHelper.Tokenize(input);
                  Sample(
                     context:ConsoleHelper.TokenizePattern(),
                     input:input,
                     output:tokens.ToEachLineString());

                  Check.That(tokens.Extracting("Action"))
                    .ContainsExactly(expected.Select(x=>x.Action));

                  Check.That(tokens.Extracting("State"))
                    .ContainsExactly(expected.Select(x=>x.State));
              });

              It("returns a ConsoleTokens Class", ()=> {

                 string input = "hello #red`word`";
                 var tokens = ConsoleHelper.Tokenize(input);

                 Check.That(tokens.GetType()).IsEqualTo(typeof(ConsoleTokens));
              });
          });

      }
    }
}
