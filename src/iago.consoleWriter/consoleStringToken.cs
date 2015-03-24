namespace Iago.ConsoleWriter
{
    public class ConsoleStringToken
    {
        public string Action {get;}
        public string State {get;}
        public ConsoleStringToken(string action, string state)
        {
            Action = action;
            State = state;
        }

        public override string ToString()
        {
            return $"{Action}('{State}')";
        }
    }
}
