namespace Iago.ConsoleWriter
{
	using System;
	
	public class OutputWrite<T>
    {
        private T input;

        private OutputWrite(T input)
        {
            this.input = input;
        }
        public static OutputWrite<T> Source(T input)
        {
            return new OutputWrite<T>(input);
        }

        public TU Then<TU>(Func<T,TU> transform)
        {
            return transform(input);
        }
    }
}