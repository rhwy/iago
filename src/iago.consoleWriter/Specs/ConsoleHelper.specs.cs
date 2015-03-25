namespace Iago.ConsoleWriter.Specs
{
    using Iago;
    using static Iago.Specs;
    using NFluent;
    using System.Linq;

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
          });

      }
    }
}
