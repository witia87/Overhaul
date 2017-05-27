using Assets.Flux.Automata;

namespace Assets.Flux.Stores
{
    public class AutomatedStore<TStateIds> : Automaton<TStateIds, Commands>
    {
        protected readonly Dispatcher Dispatcher;
        protected Automaton<TStateIds, Commands> Automaton = new Automaton<TStateIds, Commands>();

        public AutomatedStore(Dispatcher dispatcher)
        {
            Dispatcher = dispatcher;
        }

        public override IAutomaton<TStateIds, Commands> Deploy()
        {
            foreach (var transition in RegisteredTransitions)
            {
                Dispatcher.Register(transition, payload => Transition(transition, payload));
            }
            return this;
        }
    }
}