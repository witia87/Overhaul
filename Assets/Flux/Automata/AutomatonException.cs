using System;

namespace Assets.Flux.Automata
{
    public class AutomatonException : Exception
    {
        public AutomatonException()
        {
        }

        public AutomatonException(string message)
            : base("AutomatonException: " + message)
        {
        }

        public AutomatonException(string message, Exception inner)
            : base("AutomatonException: " + message, inner)
        {
        }
    }
}