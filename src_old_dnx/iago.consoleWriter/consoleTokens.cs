namespace Iago.ConsoleWriter
{
    using System.Collections.Generic;
    using System.Collections;

    public class ConsoleTokens : IEnumerable<ConsoleStringToken>
    {
        private List<ConsoleStringToken> tokens;

        public ConsoleTokens(IEnumerable<ConsoleStringToken> tokens)
        {
            this.tokens = new List<ConsoleStringToken>(tokens);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<ConsoleStringToken> GetEnumerator()
        {
            return tokens.GetEnumerator();
        }
    }

    
}
